using UnityEngine;
using UnityEngine.UI;

public class ZombieAnim : MonoBehaviour
{
    
    // references
    [SerializeField] private Locomotion locomotion;
    [SerializeField] private GameObject character;
    [SerializeField] private LayerMask melee;
    [SerializeField] private Transform upperHitCheck;
    [SerializeField] private Transform lowerHitCheck;
    [SerializeField] private LayerMask playerRender;


    // public variables that could be changed depending on situation
    public Animator charAnimator;
    public Slider slider;
    public float health;
    public float maxHealth;
    // private variables that are only used in this script
    internal bool isAlive = true;
    private bool deathAnim;
    private float hitCooldown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // death animation has not played yet
        deathAnim = false;
        // Set Health to maxhealth(100% hp)
        health = maxHealth;
        // Set hp bar value to the health
        slider.value = calculateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
        // check for sword impact using two points and radius(creates capsule)
        if (Physics.CheckCapsule(upperHitCheck.position, lowerHitCheck.position, 0.25f, melee))
        {
            if (hitCooldown == 0 && isAlive == true)
            {
                if (locomotion.attacking > 0 && locomotion.attacking <= 0.055f)
                {
                    charAnimator.SetTrigger("hit");
                    hitCooldown += 0.5f;
                    health -= 5;
                }
            }
        }

        // check if within player's 'render' distance
        if (Physics.CheckSphere(transform.position, 1, playerRender) == false)
        {
            Destroy(character);
        }




        // Set slider to current health
        slider.value = calculateHealth();

        hitCooldown -= 1 * Time.deltaTime;
        hitCooldown = Mathf.Clamp(hitCooldown, 0, 10);
        
        // if health == 0, play death function
        if (health <= 0)
        {
            // death function
            death();
        }
        // when cooldown hits 0(from animation), and isalive is false, destroy instance
        if (hitCooldown == 0 && isAlive == false)
        {
            Destroy(character);
        }


        // Debug hitcooldown
        //Debug.Log(hitCooldown);
    }

    private void death()
    {
        if (deathAnim == false)
        {
            // set cooldown(disables other actions)
            hitCooldown += 3;
            // tell script that death animation is being played
            deathAnim = true;
            // tell animator to play death animation
            charAnimator.SetTrigger("death");
            // set isalive to false
            isAlive = false;
        }

    }
    // function to calculate the % hp by dividing hp by maxhp
    float calculateHealth()
    {
        return health / maxHealth;
    }


}
