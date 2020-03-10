using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameObject GameManagerGO; // This is our game manager
    public GameObject PlayerBullet;
    public GameObject bulletPosition01;
    public GameObject bulletPosition02;
    public float speed;

    public void Init()
    {
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Fire bullets if space pressed
        if(Input.GetKeyDown("space"))
        {
            GameObject bullet01 = (GameObject)Instantiate(PlayerBullet);
            bullet01.transform.position = bulletPosition01.transform.position;

            GameObject bullet02 = (GameObject)Instantiate(PlayerBullet);
            bullet02.transform.position = bulletPosition02.transform.position;
        }

        // Move left and right depending on player input
        float x = Input.GetAxisRaw("Horizontal");
        float y = 0;

        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction);
    }

    void Move(Vector2 direction)
    {
        // Set max and min of camera view to where user sprite can go
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2((float)0.2,0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2((float)0.8,0));

        max.x = max.x - 0.5f; //half sprite width
        min.x = min.x + 0.5f;

        // Get player position
        Vector2 pos = transform.position;

        // Calculate new player position
        pos += direction * speed * Time.deltaTime;

        // Make sure position is in screen
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);

        // Update position
        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D col) 
    {
        //Detect collision of player and enemy ship or bullet
        if( (col.tag == "EnemyShipTag"))
        {
            // subtract one from health when hit
            GetComponent<Health>().updateHealth(-1);

            // if player out of lives
            if(GetComponent<Health>().health == 0) 
            {
                gameObject.SetActive(false);
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
            } 
        }    
    }
}
