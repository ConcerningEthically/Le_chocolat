using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Locomotion : MonoBehaviour
{
    // Speed
    [SerializeField] private float speed;
    // Max velocity that the object could travel(unused)
    [SerializeField] private float maxSpeed;
    // How fast the object accelerates to the max speed
    [SerializeField] private float accelerationRate;
    // How fast can the character jump
    [SerializeField] private float jumpForce;
    // Rigidbody reference
    [SerializeField] private Rigidbody playerRB;
    // Transform references
    [SerializeField] private Transform cameraOrientation;
    [SerializeField] private Transform player;
    [SerializeField] private Transform groundCheck;
    // Ground layer for collision detection
    [SerializeField] private LayerMask ground;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float rotationSpeed;
    // Animator 
    [SerializeField] private Animator charAnimator;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    
    // other Scripts
    [SerializeField] private Health health;
    
    // Character control variables
    public bool grounded;
    public float attacking;
    private bool jumping;
     
    // Vector3
    Vector3 movement;
    Vector3 angularVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Setup for program run once at the beginning of the program
        health.playerIsAlive = true;
        // Locks the mouse cursor to the middle of the screen to prevent accidental clicks
        Cursor.lockState = CursorLockMode.Locked;
        // Make sure player can jump
        jumping = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        // reset animator 
        charAnimator.SetBool("isMove", false);

        // Attacking variable for cooldowns
        attacking -= 0.1f * Time.deltaTime;
        // Prevents negative values, sets maximum
        attacking = Mathf.Clamp(attacking, 0f, 10f);
        // Debug (attacking)
        //Debug.Log(attacking);


        // Sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {

            charAnimator.SetBool("sprint", true);
            speed = sprintSpeed;
            maxSpeed = sprintSpeed;
        }
        else
        {

            charAnimator.SetBool("sprint", false);
            speed = walkSpeed;
            maxSpeed = walkSpeed;
        }

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
        //playerRB.maxLinearVelocity = maxSpeed;
        // Add movement force relative to the direction player is facing
        //playerRB.AddRelativeForce(movement * accelerationRate);

        // Old code
        float accelerationRate = 1 - (playerRB.linearVelocity.magnitude / maxSpeed);
        
        // Move player only if the player is not attacking(attack animations/modifyers move the character separately)
        // Also check if player is alive
        if (attacking == 0 && grounded == true && health.playerIsAlive == true)
        {
            playerRB.AddRelativeForce(Vector3.forward + movement * accelerationRate * speed);
        }

        // Player rotation script (first check if the character is moving)
        // Also check if player is alive
        if (movement != Vector3.zero && health.playerIsAlive == true)
        {
            // Tell animator the player is moving
            charAnimator.SetBool("isMove", true);
            // Direction player should face
            float faceDir = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cameraOrientation.eulerAngles.y;
            
            // Fixes issue when facedir becomes negative and causes character to have an epilepsy
            if (faceDir <= 0)
            {
                faceDir = 360 + faceDir;
            }

            // Debug tool
            //Debug.Log(faceDir);
            
            // Current Direction player is facing
            float currentDir = transform.eulerAngles.y;
            
            // Find the differnce between the camera direction and current direction, place the difference into a vector3
            float turn = faceDir - currentDir;
            // Fixes previous issue of having random rotations when turn value switches from positive to negative at the default 'forward' direction
            turn = Mathf.Clamp(turn, -360, 360);
            if (turn >= 180)
            {
                turn = -turn + 180;
            }
            if (turn <= -180)
            {
                turn = -turn + 180;
            }

            // Debug tool
            //Debug.Log(turn);
            // Create vector3 stating direction to turn (none, angle, none)
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
            charAnimator.SetTrigger("jump");
            playerRB.linearVelocity = new Vector3(0, jumpForce, 0);
            jumping = true;
        }

        // Stop rb from checking for gravity when on ground
        if (grounded == true)
        {
            jumping = false;
            charAnimator.SetBool("fall", false);
            charAnimator.SetBool("grounded", true);
            playerRB.useGravity = false;

        }
        if (grounded == false)
        {
            charAnimator.SetBool("grounded", false);
            if (jumping == false)
            {
                charAnimator.SetBool("fall", false);
            }
            else
            { 
                charAnimator.SetBool("fall", true); 
            }
            playerRB.useGravity = true;
        }
        return;
    }
}
