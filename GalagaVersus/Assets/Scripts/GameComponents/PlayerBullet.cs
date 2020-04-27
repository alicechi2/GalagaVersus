using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerBullet : MonoBehaviour
{
    // Establish local variable speed within the class PlayerBullet
    public float speed = 8f;
    // establish a destroyTime to destroy bullet objects and 
    public float destroyTime = 10f;
    IEnumerator destroyBullet()
    {
        yield return new WaitForSeconds(destroyTime);
        this.GetComponent<PhotonView>().RPC("destroy", RpcTarget.AllBuffered);
    }

    // Start is called before the first frame update
    void Start()
    {  
        speed = 8f; // set speed as 8f initially
        destroyTime = 10f; // set destoryTime as 10f initially
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

        //If the bullet goes off the screen, destory the gameobject
        if(transform.position.y > maxPos.y)
        {
            this.GetComponent<PhotonView>().RPC("destroy", RpcTarget.AllBuffered);
        }
    }

    // PunRPC call to destory bullet gameobject
    [PunRPC]
    public void destroy() {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "EnemyShipTag"){
            Destroy(gameObject);
        }
    }
}
