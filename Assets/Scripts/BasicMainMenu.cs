using UnityEngine;
using System.Collections;

public class BasicMainMenu : MonoBehaviour {
	// Use this for initialization
	void Awake () {
        Screen.lockCursor = false;
    }

    public void StartGame() {
        Application.LoadLevel(1);
    }

    public void EndGame() {
        Debug.Log("What happens in webgl if you try to quit the appication");
        Application.Quit();
    }

    public void OnMouseEnter() {
        GetComponent<Renderer>().material.color = Color.red;		//Change Color to red!
    }

    public void OnMouseExit() {
        GetComponent<Renderer>().material.color = Color.white;		//Change Color to white!
    }
}