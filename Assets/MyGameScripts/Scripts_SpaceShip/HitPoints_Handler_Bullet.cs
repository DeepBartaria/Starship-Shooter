using UnityEngine;

public class HitPoints_Handler_Bullet : MonoBehaviour
{
    private int HitPoints = 1;

    int return_HitPoints() {
        HitPoints--;
        return HitPoints;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggerred");
        if (other.CompareTag("SpaceShip"))
        {
            Debug.Log("ignored by buller");
            return;
        }

        if (return_HitPoints() <= 0) {
            Die();
        }
    }
    void Die() {
        Destroy(gameObject);
    }
}
