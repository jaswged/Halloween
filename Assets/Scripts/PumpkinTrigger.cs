using UnityEngine;
using System.Collections;

public class PumpkinTrigger : MonoBehaviour {
    public Material greenPumpkin;
    private Material defaultMaterial;

    public bool isSpeedPumpkin;
    public bool isTimePumpkin;

    public float timeAddedFromPumpkin = 15;

    private PumpkinRender pumpkinRenderer;

    void Awake() {
        pumpkinRenderer = GetComponentInChildren<PumpkinRender>();
    }

    void OnEnable() {
        int i = Random.Range(0, 14);
        // change stats
        if (i < 3) {
            isSpeedPumpkin = true;
            Debug.Log("Speed pumpkin spawned");
            pumpkinRenderer.ChangeColor(PumpkinRender.PumpkinColors.GreenPumpkin);
            this.gameObject.name = "SPEED Pumpkin";
        }
        else if (i < 6) {
            isTimePumpkin = true;
            // Todo change this to regular material
            pumpkinRenderer.ChangeColor(PumpkinRender.PumpkinColors.GreenPumpkin);
            Debug.Log("Time pumpkin spawned");
            this.gameObject.name = "TIME Pumpkin";
        }
        else {
            //TODO Call the PumpkinRender.ResetRenderer();
            //renderer.material = defaultMaterial;
        }
    }

    void OnDisable() {
        isTimePumpkin = false;
        isSpeedPumpkin = false;
        this.gameObject.name = "Regular Pumpkin";
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            if(isSpeedPumpkin) {
                Debug.Log("Was a Speed pumpkin. Run Faster");
                Player.player.RunFaster();
            }
            if (isTimePumpkin) {
                GameManager.manager.timer += timeAddedFromPumpkin;
                Debug.Log("Was a Time pumpkin. Additional time");
            }
            GameManager.manager.PumpkinFound(this);
            //Destroy(gameObject);
        }
    }
}