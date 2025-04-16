using UnityEngine;

public class Destructables : MonoBehaviour
{

    [SerializeField] private LayerMask weapon;
    [SerializeField] private Rigidbody obj;
    Vector3 movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider weapon)
    {
        movement = new Vector3(12,0,12);
        obj.AddForce(movement);
    }
}
