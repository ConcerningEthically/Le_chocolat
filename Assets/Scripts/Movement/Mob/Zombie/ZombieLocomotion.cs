using Unity.VisualScripting;
using UnityEngine;

public class Zombielocomotion : MonoBehaviour
{
    [SerializeField] private Transform playerChar;
    [SerializeField] private Transform zombieChar;
    [SerializeField] private Animator zombieAnim;
    [SerializeField] private Collider collision;

    // Can the Zombie see the player
    [SerializeField] private bool seePlayer;
    
    // Movement
    [SerializeField] private float walkSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Vector3 forward;

    // attacking
    [SerializeField] private float attacking;
    [SerializeField] private float attackDamage;
    [SerializeField] private float hp = 100;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Does not find the player on spawn
        seePlayer = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Changing variables(cooldowns)
        attacking -= 0.1f * Time.deltaTime;
        attacking = Mathf.Clamp(attacking, 0f, 1.0f);


        if (seePlayer == true && attacking == 0)
        {
            // Set zombie to moving animation
            zombieAnim.SetBool("isMove", true);
            

            // Target position is the player
            Vector3 Target = playerChar.position;

            // Move towards the Player(current position, target directionm, speed)
            transform.position = Vector3.MoveTowards(transform.position, Target, walkSpeed * Time.deltaTime);
            
            // Turn towards the player
            Vector3 targetDir = Target - transform.position;
            
            // Get speed for rotation
            float speed = rotateSpeed * Time.deltaTime;
            
            //Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, speed, 0.0f);
            
            transform.Rotate(speed * Vector3.up);

            // Debug(copied code), creates a ray in the direction that the character is looking
            //Debug.DrawRay(transform.position, newDirection, Color.red);

            //transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
    void Death()
    {
        
    }
}
