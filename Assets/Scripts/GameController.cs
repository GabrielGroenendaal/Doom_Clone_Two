using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the brain, the nervous system that the rest of the game will reference

public class GameController : MonoBehaviour
{
    /* REFERENCES */
    public PlayerController player;
    public AudioController audio;

    /* LEVELS */
    // public GameObject LevelOne
    
    /* PREFABS */
    public GameObject imp;
    public GameObject zombieman;
    public GameObject trooper;
    public GameObject playerPrefab;
   
    /* BOOLEAN GAMESTATES */
    private bool isPlaying;
    private bool isAlive;
    private bool update;
    public string gameState;
    
    void Start()
    {
        gameState = "main menu";
    }


    void Update()
    {
        CheckGameState();
    }
    
    /* GAME STATE CONTROL */
    public void CheckGameState()
    {
        if (gameState == "main menu")
        {
            if (update == false)
            {
                update = true;
            }
            // Press Button to start the game
            // set update to false
        }

        if (gameState == "firstLevel")
        {
            if (update == false)
            {
                audio.playClip(0);
                // Instantiate Player in Level 1
                // set player to the new playerController
                update = true;
            }
            
            if (player.health <= 0.0f)
            {
                gameState = "gameOver";
            }
            
            // Win Condition
        }

        if (gameState == "gameOver")
        {
            if (update == false)
            {
                // Instantiate 
                update = true;
            }
            // Return to Main Menu / Start of Level 
            // Set update to false
        }

        if (gameState == "victory")
        {
            if (update == false)
            {
                // Instantiate 
                update = true;
            }
            // Return to Main Menu / Start of Level 
            // Set update to false
        }
    }

    public void Pause()
    {
        // Pause the game. Stop updating?
    }

    public void Unpause()
    {
        
    }
}
