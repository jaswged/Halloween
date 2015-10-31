using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour {
    public static GameManager manager;
    private bool isLevelOver;

    public AudioClip pumpkinPickupAudioClip;

    public Maze mazePrefab;
	private Maze mazeInstance;

    public Player playerPrefab;
    private Player playerInstance;

    public float timer = 120; // 2 mins
    public float oldTimer = 120;

    public UnityEngine.UI.Text timerText;
    public UnityEngine.UI.Text pumpkinText;
    Canvas canvas;
    public Texture levelLoadingTexture;
    private bool isGeneratingMaze;
    public int fontSize = 20;

    public GameObject pumpkinPrefab;
    public int PumpkinCount {get;set;}
    public short maxPumpkins = 7;
    List<GameObject> pumpkins;    

    void Awake() { 
        manager = this;
        canvas = GameObject.Find("TimerCanvas").GetComponent<Canvas>();
    }

    void Start () {
        oldTimer = timer;
		StartCoroutine(BeginGame());
        isGeneratingMaze = true;

        pumpkins = new List<GameObject>();
        for (int i = 0; i < maxPumpkins; i++) {
            GameObject pumpkin = (GameObject) Instantiate(pumpkinPrefab);
            pumpkin.SetActive(false);
            pumpkins.Add(pumpkin);
        }
    }

    internal GameObject GetPumpkin() {
    for(int i = 0;i< pumpkins.Count;i++) {
           if(!pumpkins[i].activeInHierarchy) {
                return pumpkins[i];
            }
        }
        Debug.Log("Pumpkin list is growing. This should not happen.");
        GameObject pumpkin = (GameObject)Instantiate(pumpkinPrefab);
        pumpkin.SetActive(false);
        pumpkins.Add(pumpkin);
        return pumpkin;
    }
	
	void Update () {
        if (!isLevelOver) {
            timer -= Time.deltaTime;
        }

        DisplayTimer();
        pumpkinText.text = "Pumpkins Found: " + PumpkinCount;
       /* if (Input.GetKeyDown(KeyCode.Space)){
			RestartGame();
		}*/
	}

	private IEnumerator BeginGame(){
        isLevelOver = false;
        Camera.main.GetComponent<AudioListener>().enabled = true;
        isGeneratingMaze = true;
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
        Camera.main.GetComponent<AudioListener>().enabled = false;
        ResetTimer();
        PumpkinCount = 0;
        isGeneratingMaze = false;
        canvas.enabled = true;
    }

    internal void PumpkinFound(PumpkinTrigger pumpkinTrigger, bool isSpecialPumpkin, bool isRare) {
        AudioSource.PlayClipAtPoint(pumpkinPickupAudioClip, Player.player.transform.position, 3f);

        if (!isSpecialPumpkin) {
            PumpkinCount++;
        }
        if(isRare) {
            PumpkinCount += 2;
        }
        mazeInstance.SpawnPumpkin(pumpkinTrigger.gameObject);
    }

    private void RestartGame(){
		StopAllCoroutines();
        DisablePumpkins();
        Destroy (mazeInstance.gameObject);
        if(playerInstance != null) {
            Destroy(playerInstance.gameObject);
        }
        StartCoroutine(BeginGame());
	}

    private void DisablePumpkins() {
        //foreach(GameObject obj in pumpkins) {
        //    obj.SetActive(false);
        //}
        pumpkins.ForEach(p => p.SetActive(false)); 
    }

    private void DisplayTimer() {
        if (timer > 0) {
            String minsDisplay  = ((int)(timer / 60)).ToString();
            String secsDisplay = ((int)timer).ToString();

            int mins = (int)timer / 60;
            //int secs = (int) timer;

            if((timer - mins * 60) > 10) {
                 secsDisplay = ((int)timer - (mins * 60)).ToString();
            }
            else {
                secsDisplay = "0" + ((int)timer - mins * 60).ToString();
            }

            timerText.text = minsDisplay + " : " + secsDisplay;
        }
        else if(!isLevelOver) {
            isLevelOver = true;
            Time.timeScale = 0;
            //TODO Player.player. freeze if ^ doesn't stop player
        }
    }

    private void ResetTimer() {timer = oldTimer; }

    void OnGUI() {
        if (isGeneratingMaze) {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), levelLoadingTexture);
            canvas.enabled = false;
        }
        if(isLevelOver) {
            Cursor.lockState = CursorLockMode.None;

            GUI.skin.label.fontSize = GUI.skin.button.fontSize = fontSize;
            GUI.color = Color.white;
            //GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            //GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            //GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            //GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            //GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            //GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            //GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            //GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            //GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");

            GUILayout.BeginArea(new Rect((Screen.width - 300) / 2, (Screen.height - 200) / 2, 500, 400));

            GUILayout.Label("Congratulations!");
            GUILayout.Label("You Found " + PumpkinCount.ToString() + " pumpkins!");

            if (GUILayout.Button("Play Again!")) {
                ResetTimer();
                isLevelOver = false;
                Time.timeScale = 1;
                RestartGame();
            }

            //if (GUILayout.Button("Play Again")) {
            //    //Cursor.lockState = CursorLockMode.None;
            //    Screen.lockCursor = false;
            //    isLevelOver = false;
            //    Application.LoadLevel(Application.loadedLevel);
            //}

            GUILayout.EndArea();
        }        
    }
}