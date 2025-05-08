using System;
using UnityEngine;

public class SpaceShip_Script : All_Objects_Behaviour
{
    float maxSpeed = 5.0f;
    Vector2 moveDirection;
    float shipWidth = 0.5f;
    float shipHeight = 0.5f;


    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    public GameObject bulletPrefab3;
    float coolDownTimer = 0;
    float fireDelay = 0.25f;

    void Start()
    {
        Hitpoints = 5;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            shipWidth = spriteRenderer.bounds.extents.x;
            shipHeight = spriteRenderer.bounds.extents.y;
        }
    }

    void Update()
    {
        Move();
        Attack();
    }

    protected override void Move()
    {

        int moveX = 0;
        int moveY = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveX -= 1;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveX += 1;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveY += 1;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveY -= 1;
        }

        moveDirection = new Vector2(moveX, moveY).normalized;

        transform.Translate(moveDirection * maxSpeed * Time.deltaTime);

        Vector3 pos = transform.position;

        Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 screenTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        pos.x = Mathf.Clamp(pos.x, screenBottomLeft.x + shipWidth, screenTopRight.x - shipWidth);
        pos.y = Mathf.Clamp(pos.y, screenBottomLeft.y + shipHeight, screenTopRight.y - shipHeight);

        transform.position = pos;
    }
    protected override void Attack()
    {
        Vector3 offset = new Vector3(0, 1, 0);
        Vector3 offset2 = new Vector3(-0.6f, 0.2f, 0);
        Vector3 offset3 = new Vector3(0.6f, 0.2f, 0);
        coolDownTimer -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && coolDownTimer < 0)
        {

            coolDownTimer = fireDelay;
            Debug.Log("firing");
            try
            {
                if (bulletPrefab == null)
                {
                    throw new NullReferenceException("hay you forgot to assign bullet prefab object in inspector");
                }
                Instantiate(bulletPrefab, transform.position + offset, transform.rotation);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error : {e.Message}");
            }
            if (Astroid_Script.Total_Astroid_Destroyed > 10)
            {
                try
                {
                    if (bulletPrefab2 == null || bulletPrefab3 == null)
                    {
                        throw new NullReferenceException("hay you forgot to assign bullet prefab 2 an 3 in inspector");
                    }
                    Instantiate(bulletPrefab2, transform.position + offset2, transform.rotation);
                    Instantiate(bulletPrefab3, transform.position + offset3, transform.rotation);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error : {e.Message}");
                }
            }
        }
    }
    protected override void Respawn()
    {
    }
    protected override void Hitpoints_Handler()
    {
        

    }
    public GameMenuController gameMenuController;
    public float getHitpoints()
    {
        return Hitpoints;
    }
    public void setHitpoints()
    {
        Hitpoints++;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet2"))
        {
            Debug.Log("ignored by spacesgip");
            return;
        }
        Hitpoints--;
        if (Hitpoints <= 0)
        {
            Debug.Log("you lost");
            Destroy(gameObject);
            gameMenuController.onSpaceShipDestroied();
        }
    }
}
