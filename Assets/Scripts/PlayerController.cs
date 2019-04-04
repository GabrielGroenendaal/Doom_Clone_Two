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
        if (paused)
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
            // Movement and Camera Control

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (activeWeapon == "pistol")
                {
                    FirePistol();
                }
                
                else if (activeWeapon == "shotgun")
                {
                    FireShotgun();
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                UI.ActiveWeapon(1);
                activeWeapon = "pistol";
                // Animation
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                UI.ActiveWeapon(2);
                activeWeapon = "shotgun";
                // Animation
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UI.OpenMenu();
                game.Pause();
                paused = true;
            }
        }
    
        UI.PlayerUpdateUI(bullets, bulletsMax, shells, shellsMax, health, healthMax, armor, ArmorMax);
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
     *            if collider.GameObject.name = "shotgun"
     *                 hasShotgun = true;
     * 
     *     if (Collider.tag = "projectile")
     *
     *
 
     */
    
    /* WEAPONS */
    public void FirePistol()
    {
        audio.playClip(4);
        // Animation
    }

    public void FireShotgun()
    {
        audio.playClip(5);
        // Animation
    }
}
