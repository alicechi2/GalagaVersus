using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    // Establish local variable speed within the class PlayerBullet
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 8f;   
    }

    // Update is called once per frame
    void Update()
    {
        // Get the bullet's current position
        Vector2 position = transform.position;

        // Calculate the x, y coordinates for the new position
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        // Update the position of the bullet using the position variable above
        transform.position = position;

        // Calculate the maximum position that the bullet can travel to (end of screen)
        Vector2 maxPos = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

        // If Bullet travels outside of the frame, destroy the gameobject
        if(transform.position.y > maxPos.y)
        {
            Destroy(gameObject);
        }
    }
}
