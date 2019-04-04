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
    public GameObject LevelOne; 
    
    /* PREFABS */
    public GameObject imp;
    public GameObject zombieman;
    public GameObject trooper;
   
    /* BOOLEAN GAMESTATES */
    private bool isPlaying;
    private bool isAlive; 
    public string gameState;
    
    void Start()
    {
        gameState = "mainmenu";
    }


    void Update()
    {
        
    }
    
    /* GAME STATE CONTROL */
    /* public void CheckGameState()
     *     if (gameState = "mainmenu")
     *
     *     if (gameState = "firstLevel")
     *
     *     if (gameState = "gameOver")
     *
     *     if (gameState = "victory")
     */

    public void Pause()
    {
        
    }

    public void Unpause()
    {
        
    }
}
