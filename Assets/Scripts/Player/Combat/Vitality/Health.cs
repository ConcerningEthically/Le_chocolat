using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // Is player Alive?
    public bool playerIsAlive = true;
    
    // Main PlayerHealth variable
    public float playerHealth;
    public float maxHealth;

    // Player Health bar
    public Slider slider;
    // Player Health Amount Display
    [SerializeField] private TextMeshProUGUI textMeshPro;

    // invincibility after getting hit
    public float iFrames = 0;

    // Last Time Damage Taken
    private float timeLastHit;

    // player Regeneration rate
    // can the player regenerate health
    public bool canRegenerate = true;
    // length of time inbetween each heal
    public float playerRegenerationRate;
    // amount regenerated every time
    public float playerRegenerationAmount;
    // cooldown variable
    private float playerRegenerationCooldown = 0;


    // Health relevant stats
    public float Def;


    [SerializeField] private Animator animator;
    [SerializeField] private Locomotion locomotion;
    


    public void Start()
    {

        // default max hp is 100, changed by armour modifyers
        maxHealth = 100;
        // set playerhealth to maxhealth
        playerHealth = maxHealth;


        // player regeneration rate by default is 5 (5 seconds to recover 1 hp)
        playerRegenerationRate = 5;
        // player regerneration amount by default is 5 (5 hp per regeneration)
        playerRegenerationAmount = 5;
        // sets Def to 0 (changed by armour later)
        Def = 0;

    }
    
    public void Update()
    {
        // Cooldowns
        // Decrease Invincibility frames
        iFrames -= 0.5f * Time.deltaTime;
        iFrames = Mathf.Clamp(iFrames, 0f, 10f);
        
        // Prevent health from going into the negatives, and over the limit 
        playerHealth = Mathf.Clamp(playerHealth, 0f, maxHealth);

        // Change Last Hit value
        timeLastHit += 1 * Time.deltaTime;
        // Change Regeneration Rate value(ticks down and regenerates hp when it hits 0)
        playerRegenerationCooldown -= 1 * Time.deltaTime;
        playerRegenerationCooldown = Mathf.Clamp(playerRegenerationCooldown, 0f, playerRegenerationRate);

        // player health regeneration
        if (timeLastHit >= 10 && canRegenerate && playerRegenerationCooldown == 0)
        {
            playerHealth += playerRegenerationAmount;
            playerRegenerationCooldown += playerRegenerationRate;
        }

        // Change Hp bar
        slider.value = HpBar();
        // Change HP text 
        textMeshPro.text = "HP: " + playerHealth;



        // check if player is dead
        // play death animation, death screen
        if (playerHealth == 0 && playerIsAlive)
        {
            animator.SetTrigger("death");
            // tell locomotion script that player is dead(movement is disabled)
            playerIsAlive = false;
        }
        // (debug) Show player health
        //Debug.Log(playerHealth);
    }


    public void TakeDamage(float damageAmount, string damageType)
    {
        if (iFrames == 0)
        {
            playerHealth -= damageAmount * (1 - Def);
            timeLastHit = 0;
            
            if (damageType == "melee")
            {
                iFrames += 1f;
            }
            if (damageType == "magic")
            {
                iFrames += 0.5f;
            }
            if (damageType == "bullet")
            {
                iFrames += 0.01f;
            }
        }
    }
    public void HealDamage(float healAmount)
    {
        playerHealth -= healAmount;
    }
    float HpBar()
    {
        return playerHealth / maxHealth;
    }

}
