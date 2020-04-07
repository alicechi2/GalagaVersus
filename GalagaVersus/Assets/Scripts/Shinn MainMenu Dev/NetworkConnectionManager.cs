using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class NetworkConnectionManager : MonoBehaviourPunCallbacks
{
    // Declare the two buttons as public variables
    public Button BtnJoinRoom;
    public Button BtnConnectMaster;

    // Declare a public boolean to keep track of the state of the user attempts to 
    // connect to the master server and the join room
    public bool ConnectToMasterAttempt;
    public bool ConnectToRoomAttempt;

    // Use this for initialization
    void Start()
    {
        ConnectToMasterAttempt = false;
        ConnectToRoomAttempt = false;
    }

    // Update is called once per frame
    void Update()
    {
        // The Buttons are set to active when the server is not connected and the conditions for the connection atempts are fulfilled
        BtnConnectMaster.gameObject.SetActive(!PhotonNetwork.IsConnected && !ConnectToRoomAttempt);
        BtnJoinRoom.gameObject.SetActive(PhotonNetwork.IsConnected && !ConnectToMasterAttempt && !ConnectToRoomAttempt);
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
}
