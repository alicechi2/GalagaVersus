using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemySprite; // Our enemy prefab
    public PathCreator[] EnemyPaths = new PathCreator[20]; // Makes an array of 20 paths
    public float maxSpawnRateInSeconds = 2f; // How quickly the enemies spawn
    // Start is called before the first frame update
    int index = 0;

    private IEnumerator coroutine; // Defines a coroutine

    // Makes the WaitAndSpawn function the coroutine and then starts it
    void Start()
    {
        coroutine = WaitAndSpawn(maxSpawnRateInSeconds);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Creates and instantiates the enemiy game objects and sets each of their paths, waiting in between each creation
    private IEnumerator WaitAndSpawn(float waitTime)
    {
        while (index < EnemyPaths.Length)
        {
            yield return new WaitForSeconds(waitTime);
            GameObject anEnemy = (GameObject)Instantiate(EnemySprite);
            anEnemy.transform.position = new Vector2(-10,-10);
            anEnemy.GetComponent<EnemyControl>().enemySpawnPath = EnemyPaths[index];
            anEnemy.GetComponent<EnemyControl>().enemyID = index;
            index++;
        }
    }
}
