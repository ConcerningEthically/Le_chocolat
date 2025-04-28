using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField] private Animator charAnimator;
    [SerializeField] private Locomotion locomotion;
    [SerializeField] private Transform sword;
    [SerializeField] private Transform sheathedSword;

    public WeaponState weaponState;
    public bool sheathed;

    // reference positon for weapons that don't exist


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Gets componenet from class "Locomotion" (script)
        locomotion.GetComponent<Locomotion>();

        // Set the default to sword sheathed
        sheathed = true;
        charAnimator.SetBool("sheathed", true);
        // get createswordComponent from weapon state

    }

    // Update is called once per frame
    void Update()
    {
        //charAnimator.ResetTrigger("attack");
        
        // (Debug) bool sheathed 
        //Debug.Log(sheathed);
        
        
        // Attack script, put ahead of the unsheath script so that the char does not attack right after unsheathing
        if (Input.GetMouseButtonDown(0) && locomotion.grounded == true && locomotion.attacking == 0 && sheathed == false)
        {
            charAnimator.SetTrigger("attack");
            // Add 1 second to cooldown 
            locomotion.attacking += 0.1f;
        }
        
        // Unsheath weapon
        if (Input.GetMouseButtonDown(0) && sheathed == true || Input.GetKeyDown(KeyCode.T) && sheathed == true)
        {
            
            charAnimator.SetBool("sheathed", false);
            sheathed = false;
            locomotion.attacking += 0.2f;
            
            // delete previous sword(if any)
            weaponState.ClearInstance();

            // tell weaponstate that there isn't an instance of 'sword'
            weaponState.swordExists = false;


            // create sword at unsheathed position
            weaponState.CreateSword();


        }
        if (Input.GetKeyDown(KeyCode.T) && sheathed == false)
        {
            
            charAnimator.SetBool("sheathed", true);
            sheathed = true;
            locomotion.attacking += 0.2f;

            // delete previous sword(if any)
            weaponState.ClearInstance();
            
            // tell weaponstate that there isn't an instance of 'sword'
            weaponState.swordExists = false;


            // create sword at sheathed position
            weaponState.CreateSword();
        }

    }
}
