using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField] private Animator charAnimator;
    [SerializeField] private Locomotion locomotion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && locomotion.grounded == true)
        {
            charAnimator.SetTrigger("attack");

        }
    }
}
