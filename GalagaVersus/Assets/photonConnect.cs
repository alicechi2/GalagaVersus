using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class photonConnect : MonoBehaviour
{
    // There are no inconsistencies between different versions on the same server
    public string versionName = "0.1";
    
    public GameObject startScene, connectedScreen, lostConnectionScreen;
    
    // Use Photon's plugin to connect to the Photon server, passing in the version as a parameter
    public void  connectToPhoton(){
        PhotonNetwork.ConnectUsingSettings (versionName);
        
        Debug.Log ("Connecting to photon...");
    }
    
    // Make sure that the players get connected to the master server
    private void OnConnectedToMaster(){
        PhotonNetwrok.JoinLobby(TypedLobby.Default);
        
        Debug.Log ("Connected to master");
    }
    
    // Photon plug in for when players join the lobby
    // when player joins, he can see other servers in the game, pick up other players information, etc.
    private void OnJoinedLobby(){
        startScene.SetActive(false);
        connectedScreen.SetActive(true);
        
        Debug.Log ("On Joined Lobby");
    }
    
    // When the players are disconnected from the photon server during the middle of the game
    private void OnDisconnectedFromPhoton(){
    
        if (startScene.active)
            startScene.SetActive(false);
            
        if (connectedScreen.active)
            connectedScreen.SetActive(false);
            
        lostConnectionScreen.SetActive(true);
        
        Debug.Log ("Disconnected from photon services");
    }
}
