using UnityEngine;

public class SpaceShipMovement : MonoBehaviour
{
    //public float Hitpoints;
    float maxSpeed = 5.0f;
    Vector2 moveDirection;
    float shipWidth = 0.5f;
    float shipHeight = 0.5f;

    //public HitPoints_Display_SpaceShip HealthBar;

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null) { 
            shipWidth = spriteRenderer.bounds.extents.x;
            shipHeight = spriteRenderer.bounds.extents.y;
        }
    }
    void Update()
    {
        int moveX = 0;
        int moveY = 0;

        if (Input.GetKey(KeyCode.LeftArrow)) {
            moveX -= 1;
        }

        if (Input.GetKey(KeyCode.RightArrow)) { 
            moveX += 1;
        }

        if (Input.GetKey(KeyCode.UpArrow)) {
            moveY += 1;
        }

        if (Input.GetKey(KeyCode.DownArrow)) { 
            moveY -= 1;
        }

        moveDirection = new Vector2(moveX, moveY).normalized;

        transform.Translate(moveDirection * maxSpeed * Time.deltaTime);

        Vector3 pos = transform.position;

        Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 screenTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        // Apply padding based on half-width and half-height
        pos.x = Mathf.Clamp(pos.x, screenBottomLeft.x + shipWidth, screenTopRight.x - shipWidth);
        pos.y = Mathf.Clamp(pos.y, screenBottomLeft.y + shipHeight, screenTopRight.y - shipHeight);

        transform.position = pos;

    }
}
