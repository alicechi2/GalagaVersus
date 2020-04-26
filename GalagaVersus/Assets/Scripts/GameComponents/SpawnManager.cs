using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// Class for managing player spawns
public class SpawnManager : MonoBehaviour
{
    // declare the player sprite as a public gameobject to reference
    public GameObject playerPrefab;

    public 

    // Start is called before the first frame update
    // No need to check if player is local as the scene will only load when it is
    void Start()
    {
        // Call the spawn player function below
        SpawnPlayer();
    }

    // A function that controlls how players are spawned
    void SpawnPlayer(){
        // If there are no spawn spots that are available, raise a log error
        if (spawnSpots == null) {
            Debug.LogError("Both of the spawn spots have been occupied");
            return;
        }
        // Instantiate the position of the player randomly to be either at the top of the screen or the bottom
        SpawnSpot mySpawnSpot = spawnSpots [ Random.Range (0, spawnSpots.Length) ];

        GameObject mySpaceship = (GameObject)PhotonNetwork.Instantiate("Player Control", mySpaceship.transform.position, mySpaceship.transform.rotation);

        // Instantiate a player in the Photon Network with its name, position, and rotation
        PhotonNetwork.Instantiate(playerPrefab.name, playerPrefab.transform.position, playerPrefab.transform.rotation);
    }
}
