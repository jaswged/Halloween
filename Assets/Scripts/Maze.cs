using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {
	//public int sizeX, sizeZ;
	public IntVector2 size;

	public MazeCell cellPrefab;
    public MazePassage passagePrefab;
    public MazeWall wallPrefab;
	private MazeCell[,] cells;

	public float generateStepDelay = .001f;

	public MazeCell GetCell(IntVector2 coordinates){
		return cells[coordinates.X,coordinates.Z];
	}

	public IEnumerator Generate(){
		//WaitForSeconds delay = new WaitForSeconds(generateStepDelay);
		//Debug.Log("Generating the maze");
        float beginningTime = Time.time;

        cells = new MazeCell[size.X,size.Z];

		List<MazeCell> activeCells = new List<MazeCell>();
        activeCells.Add(CreateCell(RandomCoordinates));

		while(activeCells.Count > 0){
            yield return null; // delay;
			GenerationStep(activeCells);
		}
        float timeItTook = Time.time - beginningTime;
        Debug.Log("Maze is completed! It took " + timeItTook + " seconds.");
	}

	private void GenerationStep(List<MazeCell> activeCells){
		int currentIndex = activeCells.Count - 1;
        //currentIndex = 0;
        //currentIndex = Random.Range(0, activeCells.Count - 1);
        // Different ways of getting CurrentIndex change how the maze looks.
        
        MazeCell currentCell = activeCells[currentIndex];

        if( currentCell.IsFullyInitialized) {
            activeCells.RemoveAt(currentIndex);
            return;
        }

        MazeDirection direction = currentCell.RandomUninitializedDirection; //  MazeDirections.RandomValue;
		IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();

        if (ContainsCoordinates(coordinates)) {
            MazeCell neighbor = GetCell(coordinates);
            if (neighbor == null) {
                neighbor = CreateCell(coordinates);
                CreatePassage(currentCell, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else {
                CreateWall(currentCell, neighbor, direction);
            }
        }
        else {
            CreateWall(currentCell, null, direction);
        }
	}

	private MazeCell CreateCell(IntVector2 coordinates){
		MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
		cells[coordinates.X,coordinates.Z] = newCell;
		newCell.coordinates = coordinates;
		newCell.name = "Maze Cell " + coordinates.X + "," + coordinates.Z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3(coordinates.X- size.X * 0.5f + 0.5f, 0,  coordinates.Z - size.Z * .5f + .5f);

		return newCell;
	}

    private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        MazePassage passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }

    private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        MazeWall wall = Instantiate(wallPrefab) as MazeWall;
        wall.Initialize(cell, otherCell, direction);
        if (otherCell != null) {
            wall = Instantiate(wallPrefab) as MazeWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }

    public bool ContainsCoordinates(IntVector2 coordinate){
		return coordinate.X >= 0 && coordinate.X < size.X && coordinate.Z >=0 && coordinate.Z < size.Z;
	}

    public void AddPumpkins(short numPumpkins) {
        for (int i = 0; i < numPumpkins; i++) {            
            GameObject pumpkin = GameManager.manager.GetPumpkin();
            SpawnPumpkin(pumpkin);
        }
    }

    public void SpawnPumpkin(GameObject pumpkin) {
        //pumpkin.SetActive(false);
        //activeCells[Random.Range(0, activeCells.Count - 1)];
        int x = Random.Range(0, size.X);
        int z = Random.Range(0, size.Z);
        MazeCell currentCell = cells[x, z];
       // Debug.Log("Adding a pumpkin in cell " + x + "," + z);

        pumpkin.transform.position = currentCell.transform.position;
        pumpkin.transform.rotation = gameObject.transform.rotation;
        pumpkin.SetActive(true);
        //Instantiate(pumpkinPrefab, currentCell.transform.position, gameObject.transform.rotation);
    }
    public IntVector2 RandomCoordinates{get{return new IntVector2(Random.Range(0,size.X), Random.Range(0,size.Z));}}
}