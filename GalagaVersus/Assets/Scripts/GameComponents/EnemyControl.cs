using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject ExplosionGO;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 2f; // Set Speed
    }

    // Update is called once per frame
    void Update()
    {
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
