using UnityEngine;
using System.Collections;

public class PumpkinTrigger : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            GameManager.manager.PumpkinFound(this);
#warning should pool the pumpkins if we do the pickup as many as you can idea.
            //Destroy(gameObject);
        }
    }
}