using UnityEngine;

public class Ebullet2_Move : MonoBehaviour
{
    float bullithp = 1f;
    void Start()
    {
        
    }

    float max_speed = 5f;

    void Update()
    {
        transform.Translate(Vector2.up * max_speed * Time.deltaTime);    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bullithp--;
        if (bullithp <= 0) {
            Destroy(gameObject);
        }
    }
}
