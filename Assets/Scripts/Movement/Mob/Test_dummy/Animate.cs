using UnityEngine;
using UnityEngine.UI;

public class Animate : MonoBehaviour
{
    
    
    [SerializeField] private Locomotion locomotion;
    [SerializeField] private LayerMask melee;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private GameObject character;
    public Animator charAnimator;
    public Slider slider;
    private float hitCooldown;
    public float health;
    public float maxHealth;
    private bool isAlive = true;
    private bool deathAnim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        hitCooldown = 0;
        deathAnim = false;
        health = maxHealth;
        // Set health to max
        slider.value = calculateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
        // Set slider to current health
        slider.value = calculateHealth();

        hitCooldown -= 1 * Time.deltaTime;
        hitCooldown = Mathf.Clamp(hitCooldown, 0, 10);
        
        if (health <= 0)
        {
            death();
        }
        if (hitCooldown == 0 && isAlive == false)
        {
            Destroy(character);
        }


        // Debug hitcooldown
        //Debug.Log(hitCooldown);
    }
    private void OnTriggerEnter(Collider melee) 
    {

        if (hitCooldown == 0 && isAlive == true)
        {
            if (locomotion.attacking > 0 && locomotion.attacking <= 0.075f)
            {
                charAnimator.SetTrigger("hit");
                hitCooldown += 0.5f;
                health -= 5;
            }
        }
        
    }
    private void death()
    {
        if (deathAnim == false)
        {
            hitCooldown += 3;
            deathAnim = true;
            charAnimator.SetTrigger("death");
            isAlive = false;
        }

    }
    float calculateHealth()
    {
        return health / maxHealth;
    }
    
}
