using UnityEngine;

public class HitPoints_Handler_SpaceShip : MonoBehaviour
{
    private int HitPoints = 5;
    public int getHitpoints() {
        return HitPoints;
    }
    public void setHitpoints() {
        HitPoints++;
    }
    public GameMenuController gameMenuController;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet2")) {
            Debug.Log("ignored by spacesgip");
            return;
        }
        HitPoints--;
        if (HitPoints <= 0) {
            Debug.Log("you lost");
            Destroy(gameObject);
            gameMenuController.onSpaceShipDestroied();
        }
    }
}
