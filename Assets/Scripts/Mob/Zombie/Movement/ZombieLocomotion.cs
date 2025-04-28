using Unity.VisualScripting;
using UnityEngine;

public class ZombieLocomotion : MonoBehaviour
{
    
    [SerializeField] ZombieAnim animate;
    [SerializeField] Transform player;
    [SerializeField] Animator zombieAnim;
    [SerializeField] ZombieCombat zombieCombat;

    // If zombie is attacking
    [SerializeField] private float walkSpeed;
    
    
    void Start()
    {
        
    }

    void Update()
    {
        // Cooldowns


        // Zombie movement(zombie moves towards the player if player is within range)
        
        Vector3 moveDir = new Vector3(player.position.x, 0, player.position.z) - new Vector3(transform.position.x, 0, transform.position.z); 
        
        if (zombieCombat.hostile && zombieCombat.zombieAttack == 0 && animate.isAlive == true)
        {
            // tell zombie animator to play move animation
            zombieAnim.SetBool("isMove", true);

            // rotate to face towards the player
            transform.rotation = Quaternion.LookRotation(moveDir, Vector3.up);
            

            // move transform(position) towards the player
            transform.position = Vector3.MoveTowards(transform.position, player.position, walkSpeed * Time.deltaTime);
        }
    }


}

