using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerController : MonoBehaviour
{
    
    /* REFERENCES */
    public GameController game;
    public AudioController audio;
    public UIControl UI;
    public Camera camera;
    
    /* GAMESTATE */
    public bool isAlive;
    public bool hasShotgun;
    public bool paused;
    public string activeWeapon;
    
    /* PLAYER RESOURCES */
    public int bullets;
    public int bulletsMax;
    public int shells;
    public int shellsMax;
    public float health;
    public float healthMax;
    public float armor;
    public float ArmorMax;
    

    void Start()
    {
        health = 100;
        healthMax = 100;
        armor = 0;
        armor = 100;
        bullets = 200;
        bulletsMax = 200;
        shells = 0;
        shellsMax = 50;
        hasShotgun = false; 
        activeWeapon = "pistol";
    }

    void FixedUpdate()
    {
        if (paused = true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UI.CloseMenu();
                game.Unpause();
                paused = false;
            }
        }

        else
        {

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                
            }
        }
        // else
            
            // Movement and Camera Control
        
            // if (Input.GetKeyDown(LeftMouse) 
                // if (ActiveWeapon = "pistol")
                    // FirePistol();
                // else if (ActiveWeapon = "shotgun")
                    // FireShotgun();
            
            // if (Input.GetKeyDown(1))
                // PistolEquip();
            
            // if (Input.GetKeyDown(2))
                // ShotgunEquip();
            
            // if (Input.GetKeyDown(escape))
                // UI.OpenMenu()
                // Pause()
    
        // UI.PlayerUpdateUI(bullets, bulletsMax, shells, shellsMax, health, healthMax, armor, ArmorMax);
    }
    
    /* PICKUP */
    /*
     * public void OnCollisionEnter()
     * 
     *     if (Collider.tag = "pickup")
     *         
     *            if collider.GameObject.name = "medkit"
     *
     *                 Heal(.25);
     *                 audio.playClip(i);
     *
     *     if (Collider.tag = "projectile")
     *
     */
    
    /* WEAPONS */
    // public void ShotgunEquip()
        // UI.ActiveWeapon(2)
        // ActiveWeapon = "shotgun"
    
    // public void PistolEquip()
        // UI.ActiveWeapon(1)
        // ActiveWeapon = "pistol"
   
    
}
