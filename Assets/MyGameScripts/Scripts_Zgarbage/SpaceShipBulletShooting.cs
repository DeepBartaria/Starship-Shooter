using System;
using UnityEngine;

public class SpaceShipBulletShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    public GameObject bulletPrefab3;
    float coolDownTimer = 0;
    float fireDelay = 0.25f;
    void Start()
    {
        
    }

    void Update()
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
            catch (Exception e) {
                Debug.LogError($"Error : {e.Message}");
            }
            if (Astroid_Script.Total_Astroid_Destroyed > 10)
            {
                try
                {
                    if (bulletPrefab2 == null || bulletPrefab3 == null) {
                        throw new NullReferenceException("hay you forgot to assign bullet prefab 2 an 3 in inspector");
                    }
                    Instantiate(bulletPrefab2, transform.position + offset2, transform.rotation);
                    Instantiate(bulletPrefab3, transform.position + offset3, transform.rotation);
                }
                catch (Exception e) { 
                    Debug.LogError($"Error : {e.Message}");
                }
            }
        }
    }
}
