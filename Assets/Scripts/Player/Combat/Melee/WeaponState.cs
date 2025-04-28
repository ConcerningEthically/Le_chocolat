using UnityEngine;

public class WeaponState : MonoBehaviour
{
    // weapon object reference(for instancing later)
    [SerializeField] private GameObject weapon;
    // Locations to instantiate the sword(sheathed and unsheathed)
    [SerializeField] private Transform weaponUnsheathed;
    [SerializeField] private Transform weaponSheathed;
    // referenced script
    public AttackScript attackScript;
    // used to store the sword in a variable that could be deleted later
    public GameObject swordObject;
    // Bool swordExists to prevent a million instances of 'sword'
    public bool swordExists;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        swordExists = false;
    }
    
    public void CreateSword()
    {
        Vector3 sheathPosition = weaponSheathed.position;
        Vector3 unsheathPosition = weaponUnsheathed.position;
        
        

        // Create unsheathed sword
        if (swordExists == false && attackScript.sheathed == false)
        {
            swordObject = Instantiate(weapon, unsheathPosition, weaponUnsheathed.rotation);
            swordExists = true;
            swordObject.transform.parent = weaponUnsheathed.transform;
        }

        // Create Sheathed sword
        if (swordExists == false && attackScript.sheathed == true)
        {
            swordObject = GameObject.Instantiate(weapon, sheathPosition, weaponSheathed.rotation);
            swordExists = true;
            swordObject.transform.parent = weaponSheathed.transform;
        }

    }

    public void ClearInstance()
    {
        Destroy(swordObject);
    }

}
