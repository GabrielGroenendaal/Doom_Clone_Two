using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using System;

// This script serves as a brain for several different scripts, but the main three functions are:
//       (A) Player Movement 
//       (B) Player Inputs: Move, Shooting, Weapon Switching
//       (C) Resource Tracking 

public class PlayerController : MonoBehaviour
{
    
    /* REFERENCES TO OTHER SCRIPTS */
    public GameController game; 
    public AudioController audio;
    public UIController UI;
    
    /* GAME STATES AND BOOLEANS */
    public bool hasShotgun;
    private bool paused; // POOP
    private string activeWeapon;
    public bool blueArmor; 
    public bool greenArmor; // Tracks if player has Green Armor
    private float reloadTimer; // Timer used for reload timer between shots 
    
    /* PLAYER RESOURCES */
    public int bullets;
    private int bulletsMax;
    public int shells;
    private int shellsMax;
    public float health;
    private float healthMax;
    public float armor;
    private float ArmorMax;
    
    /*HITSCAN CODE*/
    public Raycast ray;
    private GameObject camera;
    public float poisonTimer = 0;
    
    /* */
    public GameObject pistolModel;
    public GameObject shotgunModel;

    // Initializes values of player resources, game states, and movement
    void Start()
    {
        /* RESOURCES */
        health = 100;
        healthMax = 100;
        armor = 0;
        ArmorMax = 100;
        bullets = 100;
        bulletsMax = 200;
        shells = 10;
        shellsMax = 50;
        
        /* GAME STATES */
        hasShotgun = true;
        blueArmor = false;
        greenArmor = false;
        activeWeapon = "pistol";
        UI.ActiveWeapon(0); // Sets active weapon on UI to the pistol
        pistolModel.SetActive(true);
        shotgunModel.SetActive(false);
        ray.damage = 5;
        game = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Standard FPS Movement 
    void Update()
    {
        if (health <= 0) { game.GameOver(); }
    }
    
    void FixedUpdate()
    {
        if (reloadTimer > 0.0f)
        {
            reloadTimer -= Time.deltaTime;
        } 
        
        // Input for Firing your Weapon
        if (Input.GetKeyDown(KeyCode.Mouse0) && reloadTimer <= 0.0f)
        {
            if (activeWeapon == "pistol")
            {
                if (bullets > 0) // Checks if you have bullets to fire
                {
                    audio.playClip(0); // Pistol Fire sound effect
                    reloadTimer = .25f; // Sets a short reload timer
                    bullets -= 1; // Increments Bullet Counter
                    ray.HitScanFire();
                }
            }
            else if (activeWeapon == "shotgun")
            {
                if (shells > 0)
                {
                    audio.playClip(1); // Shotgun Fire sound effect
                    reloadTimer = 0.8f; // Sets a reload timer
                    shells -= 1; // Increments Bullet Counter
                    ray.HitScanFire();
                    // Animation
                }
            }
        }
        
        // Inputs to Change Weapons
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UI.ActiveWeapon(0); // changes visual for active weapon
            audio.playClip(4);
            activeWeapon = "pistol";
            pistolModel.SetActive(true);
            shotgunModel.SetActive(false);
            ray.damage = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && hasShotgun)
        {
            UI.ActiveWeapon(1); // changes visual for active weapon
            audio.playClip(5);
            activeWeapon = "shotgun";
            pistolModel.SetActive(false);
            shotgunModel.SetActive(true);
            ray.damage = 10;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("door");
            ray.DoorFire();
        }
    
        /* UPDATES UI */
        UI.PlayerUpdateUI(activeWeapon, bullets, bulletsMax, shells, shellsMax, health, healthMax, armor, ArmorMax);
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.collider.CompareTag("Projectile"))
        {
            Damage(10);
            Debug.Log("You took damage from a projectile");
            audio.playClip(3);
            c.gameObject.SetActive(false);
        }
    }

    /* PICKUPS & COLLISION */
    public void OnTriggerEnter(Collider c)
    {
        /*
        This method parses the pickups the attached object (the player) passes through, and applies effects depending on criteria.
        The criteria examined in this code includes:
           (A) The NAME to determine the kind of pickup and its effect
           (B) The RESOURCE values that determine if the pickup has any effect
           (C) The BOOLEAN states that determine if the pickup has any effect (i.e. Blue Armor)
        
        If the criteria are met, the code then does any number of the following:
           - Increments the appropriate resource.
                  (If the associated resource is at (or close to) max capacity, it sets the resource to its maximum. This prevents overflow)
           - Plays an audio Clip through TestAudioController
           - Adjusts Boolean values for Player states 
           - Makes Calls to the UI Controller
           - Produces a Debug Log 
           - Deactivates the item Pickup 
         */
        
        if (c.CompareTag("Pickup")) 
        {
            if (c.transform.name == "Ammo Clip")
            {
                if (bulletsMax - bullets <= 10)
                {
                    bullets = bulletsMax;
                }
                else
                {
                    bullets += 10;
                }
                Debug.Log("Picked up Ammo Clip");
                audio.playClip(2);
                c.gameObject.SetActive(false);
            }
            
            else if (c.transform.name == "Bullet Box")
            {
                if (bulletsMax - bullets <= 50)
                {
                    bullets = bulletsMax;
                }
                else
                {
                    bullets += 20;
                }
                Debug.Log("Picked up Bullet Box");
                audio.playClip(2);
                c.gameObject.SetActive(false);
            }
            
            else if (c.transform.name == "Shells")
            {
                if (shellsMax - shells <= 4)
                {
                    shells = shellsMax;
                }
                else
                {
                    shells += 4;
                }
                Debug.Log("picked up Shells");
                audio.playClip(2);
                c.gameObject.SetActive(false);
            }
            
            else if (c.transform.name == "Shell Box")
            {
                if (shellsMax - shells <= 20)
                {
                    shells = shellsMax;
                }
                else
                {
                    shells += 20;
                }
                Debug.Log("picked up Shell Box");
                audio.playClip(2);
                c.gameObject.SetActive(false);
            }
            
            else if (c.transform.name == "Medikit")
            {
                if (healthMax - health <= 25)
                {
                    health = healthMax;
                }
                else
                {
                    health += 25;
                }
                Debug.Log("picked up Medikit");
                audio.playClip(2);
                c.gameObject.SetActive(false);
            }
            
            else if (c.transform.name == "Stimpack")
            {
                if (healthMax - health <= 10)
                {
                    health = healthMax;
                }
                else
                {
                    health += 10;
                }
                Debug.Log("picked up Stimpack");
                audio.playClip(2);
                c.gameObject.SetActive(false);
            }
            
            else if (c.transform.name == "Armor Bonus")
            {
                if (armor < 100)
                {
                    armor += 1;
                }
                Debug.Log("picked up Armor Bonus");
                audio.playClip(2);
                c.gameObject.SetActive(false);
            }
            
            else if (c.transform.name == "Health Bonus")
            {
                if (health < 200)
                {
                    health += 1;
                }
                Debug.Log("picked up Health Bonus");
                audio.playClip(2);
                c.gameObject.SetActive(false);
            }
            
            else if (c.transform.name == "Blue Armor")
            {
                if (armor < 200)
                {
                    armor = 200;
                    blueArmor = true;
                    greenArmor = false;
                    Debug.Log("picked up Blue Armor");
                    audio.playClip(2);
                    c.gameObject.SetActive(false);
                }
            }
            
            else if (c.transform.name == "Green Armor")
            {
                if (armor < 100)
                {
                    armor = 100;
                    greenArmor = true;
                    blueArmor = false;
                    Debug.Log("picked up Green Armor");
                    audio.playClip(2);
                    c.gameObject.SetActive(false);
                }
            }
            
            else if (c.transform.name == "Shotgun")
            {
                hasShotgun = true;
                Debug.Log("picked up Shotgun");
                UI.popUpMessage();
                audio.playClip(1);
                c.gameObject.SetActive(false);
            }
        }
        
        // Simple placeholder code for registering damage from enemies and projectile objects
        // We will replace this with Collision code on the Projectiles / Enemies, since they won't be triggers
        else if (c.CompareTag("Projectile"))
        {
            Damage(10);
            Debug.Log("You took damage from a projectile");
            audio.playClip(3);
            c.gameObject.SetActive(false);
        }
        
        else if (c.CompareTag("Final"))
        {
           game.Victory();
        }
        
        else if (c.CompareTag("Enemy"))
        {
            Damage(10);
            Debug.Log("You took damage from touching an enemy");
            audio.playClip(3);
        }
    }

    private void OnTriggerStay(Collider c)
    {
        
        if (c.transform.name == "PoisonFloor")
        {
            poisonTimer += Time.deltaTime;
            if (poisonTimer > 1f)
            {
                Damage(1);
                poisonTimer = 0;
            }
        }
    }

    public void Damage(float d)
    {
        Debug.Log("Initial Damage Taken:" + d); 
        
        if (blueArmor)
        {
            d = Mathf.Floor(d / 2);
        }
        if (greenArmor)
        {
            d = Mathf.Floor((d * 2) / 3);
        }
     
        Debug.Log("Modified Damage Taken (Armor): " + d);
        
        if (armor > d)
        {
            armor -= d;
        }
        else
        {
            float difference = d - armor;
            armor = 0;

            if (health > difference)
            {
                health -= difference;
            }
            else
            {
                health = 0;
                
            }
        }
    }
}
