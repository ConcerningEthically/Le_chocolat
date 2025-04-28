using UnityEngine;

public class ZombieCombat : MonoBehaviour
{
    [SerializeField] private ZombieLocomotion zombieLocomotion;
    [SerializeField] private Animator zombieAnimator;
    [SerializeField] internal float zombieAttack;
    [SerializeField] private GameObject player;
    [SerializeField] private ZombieAnim zombieAnim;
    
    // Zombie particular variabels
    // If zombie can see the player
    [SerializeField] internal bool hostile;
    void Start()
    {
        //hostile = false;
        zombieAttack = 0;
        hostile = true;
        
    }
    void Update()
    {
        zombieAttack -= 0.1f * Time.deltaTime;
        zombieAttack = Mathf.Clamp(zombieAttack, 0f, 10f);


    }
    private void OnTriggerEnter(Collider other)
    {
        // check if zombie can attack
        // check if zombie is hostile to player
        // check who entered the 'trigger' collider
        if (zombieAttack == 0 && hostile && zombieAnim.isAlive && other.CompareTag("Player"))
        {
            zombieAnimator.SetTrigger("attack");
            zombieAttack += 0.4f;
        }
    }
}
