using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public float scrollSpeed = 0.5f;  // Adjust this to control how fast the background moves
    private Vector2 startPosition;
    private float textureHeight;

    void Start()
    {
        startPosition = transform.position;
        textureHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        // Move the background down at the specified speed
        transform.position += Vector3.down * scrollSpeed * Time.deltaTime;

        // Check if the background has moved off the bottom of the screen
        if (transform.position.y <= startPosition.y - textureHeight)
        {
            // Reset its position to the start position to create a loop
            transform.position = new Vector2(startPosition.x, startPosition.y);
        }
    }

}
