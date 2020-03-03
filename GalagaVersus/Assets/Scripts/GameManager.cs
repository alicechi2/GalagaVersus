using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // References to our game object
    public GameObject playButton;
    public GameObject playerShip;

    public enum GameManagerState
    {
        Opening,
        Gameplay,
        GameOver,
    }

    GameManagerState GMState;
    // Start is called before the first frame update
    public void Start()
    {
        GMState = GameManagerState.Opening;
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    // Function to update the game manager state
    public void UpdateGameManagerState() 
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
                break;
            case GameManagerState.Gameplay:
                // Hides play button on starting the game
                playButton.SetActive(false);
                // Sets player as visible and initializes it
                playerShip.GetComponent<PlayerControl>().Init();
                break;
            case GameManagerState.GameOver:
                break;
        }

    }

    // Function to set the game manager state
    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    // Function that the play button will call when clicked
    // We want to drag this function into Unity in the PlayButton object and set it as the 
    // function that onClick calls.
    public void StartGameplay()
    {
        GMState = GameManagerState.Opening;
        UpdateGameManagerState();
    }
}
