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
    public GameObject pumpkinPrefab;

	public float generateStepDelay = .001f;

	public MazeCell GetCell(IntVector2 coordinates){
		return cells[coordinates.X,coordinates.Z];
	}

	public IEnumerator Generate(){
		WaitForSeconds delay = new WaitForSeconds(generateStepDelay);
		Debug.Log("Generating the maze");
        float begginningTime = Time.time;

        cells = new MazeCell[size.X,size.Z];

		List<MazeCell> activeCells = new List<MazeCell>();
		DoFirstGenerationStep(activeCells);

		while(activeCells.Count > 0){
            yield return null; // delay;
			DoNextGenerationStep(activeCells);
		}
        float timeItTook = Time.time - begginningTime;
        Debug.Log("Maze is completed! It took " + timeItTook + " seconds.");
	}

	private void DoFirstGenerationStep(List<MazeCell> activeCells){
		activeCells.Add(CreateCell(RandomCoordinates));
	}

	private void DoNextGenerationStep(List<MazeCell> activeCells){
		int currentIndex = activeCells.Count - 1;
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
            int x = Random.Range(0, size.X);
            int z = Random.Range(0, size.Z);
            MazeCell currentCell = cells[x, z];
            Debug.Log("Adding a pumpkin in cell " + x + "," + z);
            //activeCells[Random.Range(0, activeCells.Count - 1)];
            Instantiate(pumpkinPrefab, currentCell.transform.position, gameObject.transform.rotation);
        }
    }

    public IntVector2 RandomCoordinates{get{return new IntVector2(Random.Range(0,size.X), Random.Range(0,size.Z));}}
}