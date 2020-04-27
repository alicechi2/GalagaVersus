using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class PlayerName : MonoBehaviour
{
    // Declare variables to get the inputfield and button linked to setting username
    public TMP_InputField nameTF;
    public Button readyRoomBtn;

    // Make the setName Button interacatable only when user input is valid
    public void OnTFChange(TMP_Text value)
    {
        if (value.Length < 5 && value.Length > 1) {
            readyRoomBtn.interactable = true;
        }
        else {
            readyRoomBtn.interactable = false;
        }
    }

    public void OnClick_SetName()
    {
        PhotonNetwork.NickName = nameTF.text;
    }
}
