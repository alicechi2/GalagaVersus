using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 8f;   
    }

    // Update is called once per frame
    void Update()
    {
        //Get bullet curr pos
        Vector2 position = transform.position;

        //Calculate new pos
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        //Update bullet pos
        transform.position = position;

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

        //If Bullet outside sreen, destroy
        if(transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
    }
}
