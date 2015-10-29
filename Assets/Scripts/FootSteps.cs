using UnityEngine;
using System.Collections;

public class FootSteps : MonoBehaviour {
    private bool canStep;
    public AudioClip[] footsteps;

    private CharacterController controller;
    private AudioSource audioSource;

    public float walkVolume = 1f;
    public float minPitch = 0.7f;
    public float maxPitch = 1.3f;

    void Awake () {
        controller = GetComponent<CharacterController>();
        canStep = true;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (controller.isGrounded && canStep && controller.velocity.magnitude > 0.5f) {
            StartCoroutine(WalkSound()); 
        } 
    }

    IEnumerator WalkSound() {
    	canStep = false;
        audioSource.clip = footsteps[Random.Range(0, footsteps.Length)];
        audioSource.volume = walkVolume;
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.Play();
        yield return new WaitForSeconds(0.5f);
	    canStep = true;
    }

/** To call this method to stop the footsteps sound.
 *  player.SendMessage("StopFootSteps");
 */  
    public void StopFootSteps() {
        GetComponent<AudioSource>().Stop();
    }
}