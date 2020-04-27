using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

[RequireComponent(typeof(InputField))]
public class PlayerName : MonoBehaviour
{
    // Declare variables to get the inputfield and button linked to setting username
    public TMP_InputField nameTF;
    public Button readyRoomBtn;
    const string playerNamePrefKey = "PName";

    // Start is called before the first frame update
    void Start() {
        // set the defaultName to an empty string at first
        string defaultName = string.Empty;
        TMP_InputField _inputField = this.GetComponent<TMP_InputField>();
        // Check if the player already has a nickname
        // If the player does, update the defaultName and the inputField text to the nickname
        if (_inputField != null)
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                _inputField.text = defaultName;
            }
        }
        // Set the PhotonNetwork Nickname to the defaultName variable
        PhotonNetwork.NickName = defaultName;
    }

    // Make the setName Button interacatable only when user input is valid
    public void OnTFChange()
    {
        // Declare a reference to the TMP_InputField
        TMP_InputField inputField = nameTF.GetComponent<TMP_InputField>();
        // Get the value of the text within the input field
        string value = inputField.text;
        // if the inputfield is empty, make the readyRoomBtn non-interactable
        if (string.IsNullOrEmpty(value))
        {
            readyRoomBtn.interactable = false;
            Debug.LogError("Player Name is empty");
        }
        // make sure that the readyRoomBtn is only interactable if the length of the user input is less than five chars
        else if (value.Length < 5) {
            readyRoomBtn.interactable = true;
            Debug.Log(PlayerPrefs.GetString(playerNamePrefKey));
            PhotonNetwork.NickName = value;
        }
        else {
            readyRoomBtn.interactable = false;
        }
    }

    // When the user clicks the button to set username
    public void OnClick_SetName()
    {
        PhotonNetwork.NickName = nameTF.text;
    }
}
