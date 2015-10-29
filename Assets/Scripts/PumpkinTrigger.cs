using UnityEngine;
using System.Collections;

public class PumpkinTrigger : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            GameManager.manager.PumpkinFound(this);
            //Destroy(gameObject);
        }
    }
}