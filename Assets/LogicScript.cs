using UnityEngine;

public class LogicScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject zombie;
    [SerializeField] private Locomotion locomotion;
    [SerializeField] private AttackScript attackScript;
    [SerializeField] private WeaponState weaponState;
    
    public float damage;
    
    public string zombieAttackType;
    
    
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
