using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float bullet_max_speed = 10.0f;

    void Update()
    {
        transform.Translate(Vector2.up * bullet_max_speed * Time.deltaTime);
    }
}
