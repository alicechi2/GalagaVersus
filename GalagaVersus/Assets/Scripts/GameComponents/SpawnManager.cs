using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

// Class for managing player spawns
public class SpawnManager : MonoBehaviour
{
    public TMP_Text nameText; // declare the username of the player as a public variable

    // declare the player sprite as a public gameobject to reference
    public GameObject playerPrefab;
    public SpawnSpot[] spawnSpots;

    // Start is called before the first frame update
    // No need to check if player is local as the scene will only load when it is
    void Start()
    {
        nameText.text = PhotonNetwork.NickName; // set the user nickname
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
        int mySpawnSpot = Random.Range (0, spawnSpots.Length);

        // Instantiate a player in the Photon Network with its name, position, and rotation
        PhotonNetwork.Instantiate(nameText.text, playerPrefab.transform.position, playerPrefab.transform.rotation);
        playerPrefab.GetComponent<PlayerControl>().enabled = true;
    }
}
