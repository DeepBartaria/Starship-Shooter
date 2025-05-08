using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System;

public class Astroid_Script : All_Objects_Behaviour
{
    public float spawnInterval = 50;
    public float spawnRangeX = 7f;
    public float spawnYPosition = 7f;
    public GameObject asteroidPrefab;
    public static float Total_Astroid_Destroyed = 0;
    public int maxAsteroidsOnScreen = 5;
    private int totalSpawnedAsteroids = 0;
    public int maxTotalAsteroids = 5;
    private List<GameObject> asteroids = new List<GameObject>();


    public float elapsedTime = 0;
    public float stopTime = 60;
    private void Update()
    {
       Move();
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= stopTime) {
            CancelInvoke("Respawn");
        }
      
    }
    private void Start()
    {
        Hitpoints = 4;
        InvokeRepeating("Respawn", 1f, spawnInterval);
    }
    protected override void Move()
    {
        max_speed = 2;

        transform.Translate(Vector2.down * max_speed * Time.deltaTime);
    }

    protected override void Attack()
    {

    }
    protected override void Respawn()
    {
        //asteroids.RemoveAll(asteroid => asteroid == null);
        if (asteroids.Count < maxAsteroidsOnScreen && totalSpawnedAsteroids < maxTotalAsteroids)
        {
            float randomX = UnityEngine.Random.Range(-spawnRangeX, spawnRangeX);
            Vector3 spawnPosition = new Vector3(randomX, spawnYPosition, -1f);
           
            try
            {
                GameObject newAsteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
                asteroids.Add(newAsteroid);
                if (newAsteroid == null)
                {
                    throw new System.Exception("hey you, there is error in dynmaically allocatiing memory for new astroid") ;
                }
            }
            catch (SystemException e){
                Debug.LogError($"Error : {e.Message}");
            }
            totalSpawnedAsteroids++; 
        }
    }

    protected override void Hitpoints_Handler()
    {
        
    }
    float getHitpoints() {
        return Hitpoints;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hitpoints--;

        if (Hitpoints <= 0)
        {
            Total_Astroid_Destroyed++;
            Debug.Log("Astroid destroied " + Total_Astroid_Destroyed);
            Destroy(gameObject);
        }
    }
}
