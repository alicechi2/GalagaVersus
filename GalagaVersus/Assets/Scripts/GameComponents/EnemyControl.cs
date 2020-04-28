using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using Photon.Pun;

public class EnemyControl : MonoBehaviour
{
    public GameObject ExplosionGO;
    // Defines a bullet
    public GameObject EnemyBullet;
    // Defines a path
    public PathCreator enemySpawnPath;
    // 0 is for spawning mode, 1 is for stationary mode, 2 is for attack mode
    private int enemyMode;
    // The speed of the enemy traveling along the beginning path
    public float pathSpeed;
    // Total distance traveled on the beginning path
    float distanceTraveled;
    // True if the enemy reached the end of the beginning path
    public bool reachedEnd = false;
    // Length of the beginning path
    float pathLength;
    // Position of the enemy in its stationary mode
    private Vector2 stationaryPos;
    // True if the enemy arapped around the screen
    private bool wrappedAround = false;
    // Speed of the enemy when in attack mode
    public float flySpeed = 2;
    // For debugging purposes, assigns a unique ID for each enemy
    public int enemyID;
    // True if enemy should start moving in attack mode
    private bool startMoving;
    

    // Start is called before the first frame update
    void Start()
    {
        // speed = 2f; // Set Speed
        enemyMode = 0; // Sets the mode to spawn
        pathLength = enemySpawnPath.path.length;
        
    }

    // Update is called once per frame
    void Update()
    {
        // If enemy is in spawning mode
        if (enemyMode == 0) 
        {
            // Checks to see if enemy has not reached end of its path
            if (!reachedEnd)
            {
                // Determines how much of the path has been travelled
                distanceTraveled += pathSpeed*Time.deltaTime;
                // Updates the position
                Vector2 newPos = enemySpawnPath.path.GetPointAtDistance(distanceTraveled, EndOfPathInstruction.Stop);
                transform.position = newPos;
                // Checks if the enemy reached the end of its path
                if (distanceTraveled >= pathLength)
                {
                    reachedEnd = true;
                    float shootChance = Random.Range(0f, 1f);
                    // 50% chance of enemy shooting once end of path is reached
                    if (shootChance > 0.5f)
                    {
                        StartCoroutine(WaitToShoot(0f));
                    } 
                }
            }
            // If enemy has reached the end of its path, switch its mode to stationary
            else 
            {
                enemyMode = 1;
                stationaryPos = transform.position;
            }
        }
        // If enemy is in stationary mode
        else if (enemyMode == 1)
        {

            startMoving = false;
            enemyMode = 2;
            float spawnInSeconds = Random.Range(5f, 20f);
            float startShoot = Random.Range(1f, spawnInSeconds);
            // 33% chance of shooting while in stationary position
            if (startShoot> (spawnInSeconds-1)*2/3){
                StartCoroutine(WaitToShoot(startShoot));
            }
            StartCoroutine(WaitToDrop(spawnInSeconds));
            
        }
        // If enemy is in attack mode
        else if (enemyMode == 2)
        {
            // Sets boundaries
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
            
            // Starts moving the enemy if the startsMoving var is true
            if(startMoving)
            {
                // Creates position
                Vector2 position = transform.position;

                // Calculates new enemy position
                position = new Vector2(position.x, position.y-flySpeed*Time.deltaTime);

                // Update enemy position
                transform.position = position;
            }

            // Destroys sets enemy at the top of the screen if they go out of bounds on the lower end
            if (transform.position.y < min.y){
                transform.position = new Vector2(stationaryPos.x, max.y);
                wrappedAround = true;
            }

            // Sets enemy in the correct position after wrapping around and switches its mode to stationary
            else if ((transform.position.y < stationaryPos.y) && wrappedAround){
                transform.position = stationaryPos;
                wrappedAround = false;
                enemyMode = 1;
            }

            

        }
        
    }

    // Triggers when enemy collides with the player or a bullet
    void OnTriggerEnter2D(Collider2D col) 
    {
        if( (col.tag == "PlayerShipTag")  || (col.tag == "PlayerBulletTag"))
        {
            PlayExplosion();
            
            // Destroys the enemy object
            Destroy(gameObject);
        }   
    }

    // Instantiate explosion animation
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        // Set position of explosion
        explosion.transform.position = transform.position;
    }

    // Makes the enemy start moving after timeToDrop seconds
    IEnumerator WaitToDrop(float timeToDrop)
    {
        yield return new WaitForSeconds(timeToDrop);
        startMoving = true;
    }

    // Makes the enemy start shooting after timeToShoot seconds
    IEnumerator WaitToShoot(float timeToShoot)
    {
        yield return new WaitForSeconds(timeToShoot);
        // GameObject bullet = PhotonNetwork.Instantiate(EnemyBullet.name, transform.position, Quaternion.identity);
        GameObject bullet = (GameObject)Instantiate(EnemyBullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }
}
