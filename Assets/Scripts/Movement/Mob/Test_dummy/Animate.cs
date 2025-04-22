using UnityEngine;

public class Animate : MonoBehaviour
{
    
    
    [SerializeField] private Locomotion locomotion;
    [SerializeField] private LayerMask melee;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private GameObject Character;
    public Animator charAnimator;
    private float hitCooldown;
    public float health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hitCooldown = 0;
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(Character);
        }
    }
    private void OnTriggerEnter(Collider melee) 
    {
        hitCooldown -= 1 * Time.deltaTime;
        hitCooldown = Mathf.Clamp(hitCooldown, 0, 0);
        
        if (hitCooldown == 0)
        {
            if (locomotion.attacking > 0 && locomotion.attacking <= 0.075f)
            {
                charAnimator.SetTrigger("hit");
                hitCooldown += 1;
                health -= 5;
            }
        }
        
        
    }
}
