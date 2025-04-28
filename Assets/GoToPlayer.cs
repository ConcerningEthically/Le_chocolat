using UnityEngine;

public class GoToPlayer : MonoBehaviour
{
    
    [SerializeField] private Transform player;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
