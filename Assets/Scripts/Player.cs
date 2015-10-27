using UnityEngine;

public class Player : MonoBehaviour {
    private MazeCell currentCell;
    private MazeDirection currentDirection;

    CharacterController characterController;
    public float mouseSensitivity = 4.0f;
    //float verticalRotation = 0;
    public float movementSpeed = 6.0f;
   // public float runSpeedRatioMultiplier = 1.6f;
   // float verticalVelocity = 0;

    void Awake() {
        characterController = GetComponent<CharacterController>();
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

    private void Update() {
        /*if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
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
        float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotLeftRight, 0);

        float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

       // verticalVelocity += Physics.gravity.y * Time.deltaTime;

        Vector3 speed = new Vector3(sideSpeed, 0, forwardSpeed);

        speed = transform.rotation * speed;
        Debug.Log("Speed is " + speed + "   " + forwardSpeed + "    " + sideSpeed);
        //characterController.Move(speed * Time.deltaTime);
        characterController.transform.position += (speed * Time.deltaTime);
    }
}