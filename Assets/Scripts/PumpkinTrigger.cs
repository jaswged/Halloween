using UnityEngine;
using System.Collections;

public class PumpkinTrigger : MonoBehaviour {
    public Material greenPumpkin;

    public bool isGreenPumpkin;
    public bool isBluePumpkin;

    void OnEnable() {
// change stats
    }

    void OnDisable() {
        Debug.Log("Pumpkin on DIS able");
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            if(isGreenPumpkin) {
                Debug.Log("Was a green pumpkin. Additional time");
            }
            GameManager.manager.PumpkinFound(this);
            //Destroy(gameObject);
        }
    }
}