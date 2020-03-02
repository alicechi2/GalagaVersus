using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public float speed; //speed of star
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;

        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        transform.position = position;
        
        // Sets where to spawn the star
        Vector2 min = Camera.main.ViewportToWorldPoint( new Vector2(0,0));

        Vector2 max = Camera.main.ViewportToWorldPoint( new Vector2(1,1));

        //If the star goes outside the screen at the bottom,
        //then position the star at top of screen randomly positioned on x-axis
        if(transform.position.y < min.y)
        {
            transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        }
    }
}
