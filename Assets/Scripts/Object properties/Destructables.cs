using UnityEngine;

public class Destructables : MonoBehaviour
{

    [SerializeField] private LayerMask weapon;
    [SerializeField] private Collider Collider;
    [SerializeField] private Rigidbody obj;
    [SerializeField] private Locomotion locomotion;
    Vector3 movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Gets componenet from class "Locomotion" (script)
        locomotion.GetComponent<Locomotion>();
    }

    private void OnTriggerEnter(Collider weapon)
    {
        if (locomotion.attacking >= 0.015f && locomotion.attacking <= 0.075f)
        {
            movement = new Vector3(222,0,222);
            obj.AddForce(movement);
        }

    }
}
