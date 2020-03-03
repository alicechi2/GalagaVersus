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
    
    // TO-DO: Implement UI and lives

    // Reference to the lives UI Text
    public Text LivesUIText; // TO-DO: Remember to drag the text UI in Unity to set this
    const int maxLives = 3; // For the lives UI and lives system
    int lives;

    public void Init()
    {
        lives = maxLives;
        // TO-DO: Uncomment the lines below to add lives
        // LivesUIText.text = lives.ToString()
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
            lives--; // Subtracts lives
            // LivesUIText.text = lives.ToString(); // TO-DO : Uncomment
            if(lives == 0) // If player dies
            {
                gameObject.SetActive(false);
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
                
            }
            // We comment this because we just want to hide the player, not completely destroy it
            // So we can use it for rematches
            Destroy(gameObject); 
            
        }    
    }
}
