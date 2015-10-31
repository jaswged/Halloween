using UnityEngine;
using System.Collections;
using System;

public class PumpkinTrigger : MonoBehaviour {
    //public Material greenPumpkin;
    //private Material defaultMaterial;

    public bool isSpeedPumpkin;
    public bool isTimePumpkin;
    public bool isRarePumpkin;

    public float timeAddedFromPumpkin = 15;

    private PumpkinRender[] pumpkinRenderers;

    void Awake() {
        pumpkinRenderers = GetComponentsInChildren<PumpkinRender>();
    }

    void OnEnable() {
        int i = UnityEngine.Random.Range(0, 20);
        // change stats
        if (i < 5) {
            isSpeedPumpkin = true;
            Array.ForEach(pumpkinRenderers, p => p.ChangeColor(PumpkinRender.PumpkinColors.BluePumpkin));
            this.gameObject.name = "SPEED Pumpkin";
        }
        else if (i < 10) {
            isTimePumpkin = true;
            Array.ForEach(pumpkinRenderers, p =>  p.ChangeColor(PumpkinRender.PumpkinColors.GreenPumpkin));
            this.gameObject.name = "TIME Pumpkin";
        }
        else if(i < 11) {
            isRarePumpkin = true;
            Array.ForEach(pumpkinRenderers, p => p.ChangeColor(PumpkinRender.PumpkinColors.RedPumpkin));
            this.gameObject.name = "RARE_Pumpkin";
        }
        else {
            Array.ForEach(pumpkinRenderers, p => p.ChangeColor(PumpkinRender.PumpkinColors.Default));
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
            GameManager.manager.PumpkinFound(this, isSpeedPumpkin || isTimePumpkin, isRarePumpkin);
            //Destroy(gameObject);
        }
    }
}