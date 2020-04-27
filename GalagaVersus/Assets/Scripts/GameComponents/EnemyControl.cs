using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class EnemyControl : MonoBehaviour
{
    public GameObject ExplosionGO;
    float speed;
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


    // Start is called before the first frame update
    void Start()
    {
        speed = 2f; // Set Speed
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
                }
            }
            // If enemy has reached the end of its path, switch its mode to stationary
            else 
            {
                enemyMode = 1;
            }
        }
        // If enemy is in stationary mode
        else if (enemyMode == 1)
        {
            // TO-DO: Implement the stationary mode.
            // Debug.Log("hi");
        }
        // If enemy is in attack mode
        else
        {
            // TO-DO: Implement the attack mode
            // Creates position
            Vector2 position = transform.position;

            // Calculates new enemy position
            position = new Vector2(position.x, position.y-speed*Time.deltaTime);

            // Update enemy position
            transform.position = position;

            // Sets boundaries
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));

            // Destroys enemy if they go outside the boundary
            if (transform.position.y < min.y){
                Destroy(gameObject);
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
}
