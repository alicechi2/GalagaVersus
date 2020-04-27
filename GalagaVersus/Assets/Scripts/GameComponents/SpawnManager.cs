using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// Class for managing player spawns
public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnSpots;

    // declare game setup variable
    public static SpawnManager GS;

    // An event function that runs when this script is enabled
    private void OnEnable() {
        if (SpawnManager.GS == null)
        {
            SpawnManager.GS = this;
        }
    }
}
