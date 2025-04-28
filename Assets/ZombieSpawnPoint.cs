using UnityEngine;

public class ZombieSpawnPoint : MonoBehaviour
{
    // store how much seconds left of timer
    [SerializeField] private float timer;
    // what object to spawn
    [SerializeField] private GameObject zombie;
    // where to spawn zombie
    [SerializeField] private Transform spawnPos;
    // cooldown per zombie spawn
    [SerializeField] private float spawnCooldown;

    // Player 'render' distance
    [SerializeField] private LayerMask playerRender;

    // maximum zombies that could spawn at a time 
    public int maxZombies;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= 1 * Time.deltaTime;
        timer = Mathf.Clamp(timer, 0f, 10f);

        GameObject [] zombies = GameObject.FindGameObjectsWithTag("Zombies");
        int zombieAmount =  zombies.Length;
        //Debug.Log(zombieAmount);

        // check if in render distance
        

        if (timer == 0 && zombieAmount < maxZombies && Physics.CheckSphere(transform.position, 1, playerRender))
        {
            Instantiate(zombie, spawnPos.position, spawnPos.rotation);
            timer += spawnCooldown;
        }
    }
}
