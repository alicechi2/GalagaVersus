using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class photonConnect : MonoBehaviour
{
    // Restrict players with different versions of the game to
    // join the same server
    public string versionName = "0.1";

    public GameObject connectPanel;
    public GameObject joinPanel;
    public GameObject disconnectPanel;

    // The function that allows players to access the server
    // Passes in the versionName as a parameter
    public void connectToPhoton(){
        PhotonNetwork.ConnectUsingSettings(versionName);

        Debug.Log("Connecting to Photon server...");
    }

    // When players join the master server or the default game lobby,
    // they should be able to see the username of the other players in the lobby
    public void OnConnectedToMaster(){
        PhotonNetwork.JoinLobby(TypedLobby.Default);

        Debug.Log("We are connected to master");
    }

    // A function that lets users join a lobby
    private void OnJoinedLobby(){
        // When the player joins the lobby, the panel with the button
        // to join the lobby should no longer be active, and the lobby
        // panel should be activated
        connectPanel.SetActive(false);
        joinPanel.SetActive(true);

        Debug.Log("On Joined Lobby");
    }

    // When the player gets disconnected from the server, run this funciton
    private void OnDisconnectedFromPhoton(){

        // Only set active the panel that displays the error message
        if(connectPanel.SetActive(true))
            connectPanel.SetActive(false);

        if(joinPanel.SetActive(true))
            joinPanel.SetActive(false);

        disconnectPanel.SetActive(true);

        Debug.Log("Disconnected from photon services");
    }
}
