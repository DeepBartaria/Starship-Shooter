using UnityEngine;

public class Ebullet1_Move : MonoBehaviour
{
    float maxSpeed = 10;
    void Start()
    {
        
    }
    void Update()
    {
        transform.Translate(Vector2.down * maxSpeed * Time.deltaTime);
    }
}
