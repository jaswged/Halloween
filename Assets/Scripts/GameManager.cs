using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {
    public static GameManager manager;

    public Maze mazePrefab;
	private Maze mazeInstance;

    public Player playerPrefab;
    private Player playerInstance;

    public float timer = 180; // 3 mins
    private float oldTimer;
    private bool isLevelFinished = false;

    public UnityEngine.UI.Text timerText;
    public UnityEngine.UI.Text pumpkinText;
    private bool isGeneratingMaze;

    public int PumpkinCount {get;set;}
    public short maxPumpkins = 3;
	#warning need to refactor this name

    void Awake() { manager = this; }

    void Start () {
        oldTimer = timer;
		StartCoroutine(BeginGame());
        isGeneratingMaze = true;
        //PumpkinCount = maxPumpkins;
    }
	
	void Update () {
        if (!isLevelFinished) {
            timer -= Time.deltaTime;
        }

        DisplayTimer();
        pumpkinText.text = "Pumpkins Found: " + PumpkinCount;

        if (Input.GetKeyDown(KeyCode.Space)){
			RestartGame();
		}
	}

	private IEnumerator BeginGame(){
        Camera.main.clearFlags = CameraClearFlags.Skybox;
        Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
		mazeInstance = Instantiate(mazePrefab) as Maze;
        yield return StartCoroutine(mazeInstance.Generate());

// Maze has been completed.
        mazeInstance.AddPumpkins(maxPumpkins);
        playerInstance = Instantiate(playerPrefab) as Player;
        playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
        Camera.main.clearFlags = CameraClearFlags.Depth;
        Camera.main.rect = new Rect(0f, 0f, .5f, .5f);
        ResetTimer();
        isGeneratingMaze = false;


    }

	private void RestartGame(){
		StopAllCoroutines();
        isGeneratingMaze = true;
        Destroy (mazeInstance.gameObject);
        if(playerInstance != null) {
            Destroy(playerInstance.gameObject);
        }
        StartCoroutine(BeginGame());
	}

    private void DisplayTimer() {
        if (timer > 0) {
            String minsDisplay  = ((int)(timer / 60)).ToString();
            String secsDisplay = ((int)timer).ToString();

            int mins = (int)timer / 60;
            int secs = (int) timer;

            if((timer - mins * 60) > 10) {
                 secsDisplay = ((int)timer - (mins * 60)).ToString();
            }
            else {
                secsDisplay = "0" + ((int)timer - mins * 60).ToString();
            }

            timerText.text = minsDisplay + " : " + secsDisplay;
        }
        else {
            ResetTimer();
        }
    }

    private void ResetTimer() {timer = oldTimer; }

    void OnGUI() {
        if (isGeneratingMaze) return;

        // Show maze generating image/texture here like the blood overlay.
    }
}