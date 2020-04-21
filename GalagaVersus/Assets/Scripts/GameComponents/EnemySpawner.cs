using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemySprite; // Our enemy prefab
    float maxSpawnRateInSeconds = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnEnemy", maxSpawnRateInSeconds);

        // increase spawn rate every 30 s
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        // Sets where to spawn the enemy
        Vector2 min = Camera.main.ViewportToWorldPoint( new Vector2((float) 0.25,0));

        Vector2 max = Camera.main.ViewportToWorldPoint( new Vector2((float) 0.75,1));

        // Spawns the enemy
        GameObject anEnemy = (GameObject)Instantiate(EnemySprite);
        anEnemy.transform.position = new Vector2(Random.Range (min.x, max.x), max.y);

        // Schedule when to spawn enemy
        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn()
    {
        // Determines when to spawn the enemy
        float spawnInSeconds;
        if (maxSpawnRateInSeconds > 1f)
        {
            spawnInSeconds = Random.Range(1f, maxSpawnRateInSeconds);
        }
        else {
            spawnInSeconds = 1f;
        } 
        Invoke("SpawnEnemy", spawnInSeconds);
    }

    // For a hard mode i think
    void IncreaseSpawnRate() {
        if (maxSpawnRateInSeconds > 1f){
            maxSpawnRateInSeconds--;
        } if (maxSpawnRateInSeconds == 1f){
            CancelInvoke("IncreaseSpawnRate");
        }
    }
}
