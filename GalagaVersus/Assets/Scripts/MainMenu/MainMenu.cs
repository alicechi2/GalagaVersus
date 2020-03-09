using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MainMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject findOpponentPanel = null;
    [SerializeField] private GameObject waitingStatusPanel = null;
    // [SerializeField] private GameObject waitingStatusText = null;

    private bool isConnecting = false;

    // Makes sure that we can only be matched with others on the same version
    private const string GameVersion = "0.1";
    private const int MaxPlayersPerRoom = 2;

    private void Awake() {
        // When all players connect, scenes sync
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void FindOpponent()
    {
        isConnecting = true;
        findOpponentPanel.SetActive(false);
        waitingStatusPanel.SetActive(true);

        // waitingStatusText.text = "Searching...";

        // We connect to a random room
        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.GameVersion = GameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master");
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    // If player disconnects
    public override void OnDisconnected(DisconnectCause cause)
    {
        // waitingStatusPanel.SetActive(false);
        findOpponentPanel.SetActive(true);

        Debug.Log($"Disconnected due to {cause}");
    }

    // Random joining fails
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No clients are waiting for an opponent, creating a new room");

        PhotonNetwork.CreateRoom(null, new RoomOptions{MaxPlayers = MaxPlayersPerRoom});
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Client successfully joined the room");

        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if(playerCount != MaxPlayersPerRoom)
        {
            // waitingStatusText.text = "Waiting For Opponent";
            Debug.Log("Client is waiting for a room");
        }
        else
        {
            // waitingStatusText.text = "Opponent Found";
            Debug.Log("Matching is ready to begin");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayersPerRoom)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;

            // waitingStatusText.text = "Opponent Found";
            Debug.Log("Match is ready to begin");
            
            PhotonNetwork.LoadLevel("GameEngine");
        }
    }

    // Start is called before the first frame update\
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
