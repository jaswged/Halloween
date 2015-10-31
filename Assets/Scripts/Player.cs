using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {
    private MazeCell currentCell;
    private MazeDirection currentDirection;

    public static Player player;

    /*CharacterController characterController;
    public float mouseSensitivity = 4.0f;
    public float movementSpeed = 6.0f;*/
    //float verticalRotation = 0;
    // public float runSpeedRatioMultiplier = 1.6f;
    // float verticalVelocity = 0;

    #region toDelete
    public float movementSpeed = 1.2f;// 6
    public float baseMovementSpeed = 1.2f;
    public float mouseSensitivity = 4.0f;
    public float jumpSpeed = 3.0f;
    public float upDownRange = 60.0f;
    public float runSpeedRatioMultiplier = 1.6f;
    public float inAirRatio = 0.8f;

    float verticalRotation = 0;
    CharacterController characterController;
    float verticalVelocity = 0;
    #endregion

    void Awake() {
        characterController = GetComponent<CharacterController>();
        player = this;
    }

    public void disableController() {
// Might delete
    }

    public void SetLocation(MazeCell cell) {
        currentCell = cell;
        transform.localPosition = cell.transform.localPosition;
    }

    private void Rotate(MazeDirection direction) {
        transform.localRotation = direction.ToRotation();
        currentDirection = direction;
    }

    private void Move(MazeDirection direction) {
        MazeCellEdge edge = currentCell.GetEdge(direction);
        if (edge is MazePassage) {
            SetLocation(edge.otherCell);
        }
    }

    internal void RunFaster() {
        StartCoroutine(IncreaseMovementSpeedTemporarily());
    }

    void Update() {
        #region Rotation
        float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotLeftRight, 0);
        //verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
       // verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
       // Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        #endregion
        #region Movement
        float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        /*if (Input.GetButton("run")) {
            forwardSpeed *= runSpeedRatioMultiplier;
        }*/
        float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

        verticalVelocity += Physics.gravity.y * Time.deltaTime;
    
        Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);

        speed = transform.rotation * speed;

        characterController.Move(speed * Time.deltaTime);
        #endregion
    }

    public IEnumerator IncreaseMovementSpeedTemporarily() {
        Debug.Log("Run faster for 10 seconds");
        movementSpeed *= 2;
        yield return new WaitForSeconds(10);
        movementSpeed = baseMovementSpeed;
    }

    /*void Update() {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            Move(currentDirection);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            Move(currentDirection.GetNextClockwise());
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            Move(currentDirection.GetOpposite());
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            Move(currentDirection.GetNextCounterclockwise());
        }
        else if (Input.GetKeyDown(KeyCode.Q)) {
            Rotate(currentDirection.GetNextCounterclockwise());// was Look()
        }
        else if (Input.GetKeyDown(KeyCode.E)) {
            Rotate(currentDirection.GetNextClockwise()); // was Look()
        }*/
    /* float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
         transform.Rotate(0, rotLeftRight, 0);

         float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
         float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;
         Debug.Log("Horizontal is " + Input.GetAxis("Horizontal"));
        // verticalVelocity += Physics.gravity.y * Time.deltaTime;

         Vector3 speed = new Vector3(sideSpeed, 0, forwardSpeed);

         speed = transform.rotation * speed * Time.deltaTime;
         //Debug.Log("Speed is " + speed + "   " + forwardSpeed + "    " + sideSpeed);
         //characterController.Move(speed * Time.deltaTime);
         characterController.transform.Translate(speed);
     }*/
}