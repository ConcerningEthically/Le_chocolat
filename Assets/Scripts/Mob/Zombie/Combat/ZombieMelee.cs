using UnityEngine;

public class ZombieMelee : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private GameObject player;
    [SerializeField] private ZombieLocomotion zombielocomotion;
    [SerializeField] private ZombieCombat zombieCombat;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (zombieCombat.zombieAttack > 0.15f && zombieCombat.zombieAttack < 0.35f)
        {
            health.TakeDamage(10, "melee");
        }
    }
}
