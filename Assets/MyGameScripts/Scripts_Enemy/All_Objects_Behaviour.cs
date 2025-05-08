using UnityEngine;

public abstract class All_Objects_Behaviour : MonoBehaviour
{
    protected float Hitpoints;
    protected float damage;
    protected float max_speed;
    protected static float score = 0;
    protected abstract void Move();
    protected abstract void Attack();
    protected abstract void Respawn();
    protected abstract void Hitpoints_Handler();
}
