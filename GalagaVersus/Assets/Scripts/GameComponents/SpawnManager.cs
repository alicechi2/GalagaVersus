using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// Class for managing player spawns
public class SpawnManager : MonoBehaviour
{
    // declare the player sprite as a public gameobject to reference
    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Call the spawn player function below
        SpawnPlayer();
    }

    // A function that controlls how players are spawned
    void SpawnPlayer(){
        // Instantiate a player in the Photon Network with its name, position, and rotation
        PhotonNetwork.Instantiate(playerPrefab.name, playerPrefab.transform.position, playerPrefab.transform.rotation);
    }
}
