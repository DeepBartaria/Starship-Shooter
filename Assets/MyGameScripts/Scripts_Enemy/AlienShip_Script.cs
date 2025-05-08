using UnityEngine;

public class AlienShip_Script : All_Objects_Behaviour
{
    public static float Total_AlienShip_Destroyed = 0;
    public GameObject alienShipPrefab;
    public GameObject bulletPrefab;
    private float elapsedTime = 0;
    private float stopSpawningTime = 65;
    void Start()
    {
        Hitpoints = 30;
        InvokeRepeating("Respawn", 15f, 20f);
    }

    
    void Update()
    {
        Move();
        Attack();
        elapsedTime += Time.deltaTime;

        if (elapsedTime > stopSpawningTime) {
            CancelInvoke("Respawn");
        }
    }

    protected override void Move()
    {
        max_speed = 0.6f;

        transform.Translate(Vector2.left * max_speed * Time.deltaTime);
    }

    float coolDownTime = 0;
    float delayTime = 3;
    protected override void Attack()
    {
        coolDownTime -= Time.deltaTime;
        Vector3 offset = new Vector3(0, 1, 0);
        if (coolDownTime < 0) {
            coolDownTime = delayTime;
            Instantiate(bulletPrefab, transform.position - offset, Quaternion.identity);
        }
    }

    protected override void Respawn()
    {
        float randomx = Random.Range(-7f, 7f);
        Vector3 SpawnPosition = new Vector3(randomx, 5f, -1f);
        Quaternion spawnRotation = Quaternion.Euler(0, 0, 90);
        try
        {
            GameObject newAlienShip = Instantiate(alienShipPrefab, SpawnPosition, spawnRotation);
            if (newAlienShip == null)
            {
                throw new System.Exception("hey therr is error in dynamically allocating memory for Alien Ship");
            }
        }
        catch (System.Exception e) {
            Debug.LogError($"Error : {e.Message}");
        }

    }

    protected override void Hitpoints_Handler()
    {

    }
     
    void OnTriggerEnter2D()
    {
        Hitpoints--;
        if (Hitpoints <= 0) {
            Total_AlienShip_Destroyed += 5;
            Debug.Log("Total alien ship destroyed " + Total_AlienShip_Destroyed);
            Destroy(gameObject);
        }
    }
}
