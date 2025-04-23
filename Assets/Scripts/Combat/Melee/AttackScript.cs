using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField] private Animator charAnimator;
    [SerializeField] private Locomotion locomotion;
    [SerializeField] private Transform sword;
    [SerializeField] private Transform sheathedSword;
    [SerializeField] private bool sheathed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Gets componenet from class "Locomotion" (script)
        locomotion.GetComponent<Locomotion>();
        
        // Set the default to sword sheathed
        sheathed = true;


        // smth here that basically does sword.hidden = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && sheathed == true || Input.GetKeyDown(KeyCode.T))
        {
            
        }

        if (Input.GetMouseButtonDown(0) && locomotion.grounded == true && locomotion.attacking == 0 && sheathed == false)
        {
            charAnimator.SetTrigger("attack");
            locomotion.attacking += 0.1f;
        }

    }
}
