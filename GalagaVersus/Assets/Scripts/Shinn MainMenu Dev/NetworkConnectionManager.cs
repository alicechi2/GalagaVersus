using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkConnectionManager : MonoBehaviourPunCallbacks
{
    // Declare the MainMenu Gameobjects as public variables
    public Button BtnJoinRoom;
    public Button BtnConnectMaster;
    public Canvas CanvasDisconnectFromMaster;
    public Button BtnJoinWhenDisconnect;
    public Button BtnQuitWhenDisconnect;
    public Text TextDisconnectNotifier;

    // Declare a public boolean to keep track of the state of the user attempts to 
    // connect to the master server and the join room
    public bool ConnectToMasterAttempt;
    public bool ConnectToRoomAttempt;
    public bool Disconnected;
    public bool CanvasDisplayed;

    // Data Values for Room settings
    [SerializeField]
    private byte maxPlayersPerRoom = 2;
    [SerializeField]
    private bool roomVisible = true;

    // Use this for initialization
    void Start()
    {
        // Initially set these four to false
        ConnectToMasterAttempt = false;
        ConnectToRoomAttempt = false;
        Disconnected = false;
        CanvasDisplayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        // The Buttons are set to active when the server is not connected and the conditions for the connection attempts are fulfilled
        BtnConnectMaster.gameObject.SetActive(!PhotonNetwork.IsConnected && !ConnectToRoomAttempt);
        BtnJoinRoom.gameObject.SetActive(PhotonNetwork.IsConnected && !ConnectToMasterAttempt && !ConnectToRoomAttempt);

        // The Canvas has to be set active when all the conditions are true, and the game is not connected to the Photon server
        CanvasDisconnectFromMaster.gameObject.SetActive(!PhotonNetwork.IsConnected && !Disconnected);
        // The buttons and text on the canvas are set active when the game is disconnected and the canvas is displayed
        BtnJoinWhenDisconnect.gameObject.SetActive(!Disconnected && !CanvasDisplayed);
        BtnQuitWhenDisconnect.gameObject.SetActive(!Disconnected && !CanvasDisplayed);
        TextDisconnectNotifier.gameObject.SetActive(!Disconnected && !CanvasDisplayed);
    }

    // When the button ConnectToMaster is pressed
    public void OnClickConnectToMaster() 
    {
        // Settings for configuring the Photon Network
        PhotonNetwork.OfflineMode = false; // Setting this to truw ould fake an online connection
        PhotonNetwork.NickName = "PlayerName"; // This is the settings to configure a player nickname
        PhotonNetwork.AutomaticallySyncScene  = true; //to call PhotonNetwork.LoadLevel()
        PhotonNetwork.GameVersion = "v1"; // This is to restrict players with different versions from playing together

        ConnectToMasterAttempt = true;
        // In case the automatic connection fails to work this is the function call for the manual connection
        // PhotonNetwork.ConnectToMaster(ip, port, 62e2b6f7-09e9-495c-92fa-24bf602b565d)
        PhotonNetwork.ConnectUsingSettings(); //automatic connection based on the config file in Photon/PhotonUnityNetworking/Resourcs/PhotonServerSettings.asset
    }

    // When the something goes wrong with connecting to the server
    public override void OnDisconnected(DisconnectCause cause) {
        base.OnDisconnected(cause);

        // As soon as disconnected, set these conditions to false
        ConnectToMasterAttempt = false;
        ConnectToRoomAttempt = false;

        // Set the disconnected condition to be true
        Disconnected = true;

        // Print out on the console to see what is wrong
        Debug.Log(cause);
    }

    // When the user successfully connects to the master server
    public override void OnConnectedToMaster() 
    {
        base.OnConnectedToMaster();
        // There are no more attempts necessary, so set this condition to false
        ConnectToMasterAttempt = false;
        // Print out that connection was successful
         Debug.Log("Connection successful to Master server");
    }

    // When the user clicks the button to connect to a room
    public void OnClickConnectToRoom()
    {
        // If the Photon Network is not connected, we should return
        if (!PhotonNetwork.IsConnected) {
            return;
        }

        ConnectToRoomAttempt = true;

        // Have Photon guide the suer to a random room
        PhotonNetwork.JoinRandomRoom(); // - Error: OnJoinedRandomRoomFailed
    }

    // When the user is able to join a random room, then this function is called
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        ConnectToRoomAttempt = true;

        // One player is always set as master so that he or she can make executive decisions
        // On the console, print out the number of players who are currently in the game lobby
        Debug.Log("Master: " + PhotonNetwork.IsMasterClient + " | PLayers in Room: " + PhotonNetwork.CurrentRoom.PlayerCount);
        // After the Room is Joined, it will launch the scene GameEngine to play the game
        SceneManager.LoadScene("GameEngine"); 
    }

    // When JoinRandomRoom() fails which means that there are no available rooms for the player
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        ConnectToRoomAttempt = true;

        // This could signify that no rooms are currently available
        // Create a new join room where the name of the room is temporarily set to null
        PhotonNetwork.CreateRoom(null, new RoomOptions {
            MaxPlayers = maxPlayersPerRoom, // max players who can join must be two
            PlayerTtl = 30000, // Time To Live: Player (If player is inactive for 30 seconds,remove from room)
            EmptyRoomTtl = 30000, // Time To Live: Room (Keep room in memory and remove room 30 seconds after last player leaves)
            IsVisible = roomVisible // Set the visibility of the room to player's choice (either public or private)
            });
    }

    // When the player is unable to crete a new room. For example, when the player passes a room
    // condition that violates rules set forth by the Photon server
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log(message);
        ConnectToRoomAttempt = false;
    }

    // When the user gets disconnected and decides to Quit the game
    public override void OnLeftLobby()
    {
        base.OnLeftLobby();
        // If the player decides to quit, set disconnect to false again
        Disconnected = false;

        // Print out that the user decided to quit
         Debug.Log("Player chose to quit game when server disconnected");
    }
}
