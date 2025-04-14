using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Locomotion : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float accelerationRate;
    [SerializeField] private float jumpForce;
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private Transform cameraOrientation;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] Vector3 angularVelocity;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Animator charAnimator;
    private bool grounded;
    public Vector3 movement;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Setup for program run once at the beginning of the program
        // Locks the mouse cursor to the middle of the screen to prevent accidental clicks
        Cursor.lockState = CursorLockMode.Locked;

        
    }

    // Update is called once per frame
    void Update()
    {
        // reset animator 
        charAnimator.SetBool("isMove", false);


        // Personal preference to have a key to unlock mouse
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            // Unlock Mouse from middle of screen
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            // Lock Mouse from middle of screen
            Cursor.lockState = CursorLockMode.Locked;
        }
        

        // Movement
        // Get user inputs WASD, Arrow keys
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Vector3 movement (x input, 0, z input) normalized (turns the coordinate values to a direction vector value 1)
        movement = new Vector3(x, 0f, z).normalized;
        // States max velocity that the object could move
        playerRB.maxLinearVelocity = maxSpeed;
        // Add movement force relative to the direction player is facing
        playerRB.AddRelativeForce(movement * accelerationRate);

        // Old code
        // float accelerationRate = 1 - (playerRB.linearVelocity.magnitude / maxSpeed);

        // playerRB.AddRelativeForce(movement * accelerationRate * speed);

        // player rotation script
        if (movement != Vector3.zero)
        {
            // tell animator the player is moving
            charAnimator.SetBool("isMove", true);
            // Direction player should face
            float faceDir = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cameraOrientation.eulerAngles.y;
            //Debug.Log(faceDir);
            // Current Direction player is facing
            float currentDir = transform.eulerAngles.y;
            float turn = faceDir - currentDir;

            // Find the differnce between the camera direction and current direction, place the difference into a vector3

            //Debug.Log(turn);
            angularVelocity = new Vector3(0, turn, 0);
            // Set a limit to how fast the character turns(speed(amount) to turn * Time.deltatime)
            Quaternion deltaRotation = Quaternion.Euler(angularVelocity * Time.fixedDeltaTime * rotationSpeed);
            // Rotate the player to remove the delta
            playerRB.MoveRotation(playerRB.rotation * deltaRotation);            
        }



        // Jump Mechanics
        // Check if the player is grounded
        grounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, ground);
        // Debug if the player is grounded
        //Debug.Log(grounded);

        // Change y velocity when space is pressed(jump)
        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {
            playerRB.linearVelocity = new Vector3(0, jumpForce, 0);
        }

        // Stop rb from checking for gravity when on ground
        if (grounded == true)
        {
            playerRB.useGravity = false;
        }
        if (grounded == false)
        {
            playerRB.useGravity = true;
        }
        return;
    }
}
