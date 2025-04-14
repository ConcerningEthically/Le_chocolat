using UnityEngine;

public class Animation : MonoBehaviour
{
    [SerializeField] private Locomotion locomotion;
    [SerializeField] private Animator charAnimator;
    
    void Start()
    {
        locomotion = locomotion.GetComponent<Locomotion>();
    }

    void Update()
    {
        if (locomotion.movement != Vector3.zero)
        {
            charAnimator.SetBool("isMove", true);
        }
        if (locomotion.movement == Vector3.zero)
        {
            charAnimator.SetBool("isMove", false);
        }
    }
}
