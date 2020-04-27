using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// Class for managing player spawns
public class SpawnManager : MonoBehaviour
{
    // make a PhotonView reference
    private PhotonView pv;
    // register all of the spawnSpots
    public Transform[] spawnSpots;

    // declare the Spaceship object
    public GameObject playerPrefab;

    // declare game setup variable
    public static SpawnManager GS;

    // An event function that runs when this script is enabled
    private void OnEnable() {
        if (SpawnManager.GS == null)
        {
            SpawnManager.GS = this;
        }
    }
    // Use this for initialization
    void Start() {
        pv = GetComponent<PhotonView>();
        if (pv.IsMine)
        {
            SpawnPlayer();
        }
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
        PhotonNetwork.Instantiate(playerPrefab.name, spawnSpots[mySpawnSpot].position, 
            spawnSpots[mySpawnSpot].rotation, 0);
        playerPrefab.GetComponent<PlayerControl>().enabled = true;
    }
}
