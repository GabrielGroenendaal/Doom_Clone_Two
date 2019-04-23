using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

// This script serves as a brain for several different scripts, but the main three functions are:
//       (A) Player Movement 
//       (B) Player Inputs: Move, Shooting, Weapon Switching
//       (C) Resource Tracking 
// This script references two other major scripts:
//       | TestUIController which displays all the resources tracked in this script on the UI
//       | TestAudioController which manages audio clips that are referenced by this script

/* POOP */
// Any script with the comment "POOP" is not currently functional / implemented. 

public class TestPlayerController : MonoBehaviour
{
    
    /* REFERENCES TO OTHER SCRIPTS */
    public GameController game; // POOP - Will be important but we don't have a central gameController yet
    public TestAudioController audio;
    public TestUIController UI;
    
    /* GAME STATES AND BOOLEANS */
    // A handful of booleans and timer that track different game and player states as they get upgrades
    public bool hasShotgun;
    public bool paused; // POOP
    public string activeWeapon;
    public bool blueArmor; 
    public bool greenArmor; // Tracks if player has Green Armor
    public float reloadTimer; // Timer used for reload timer between shots 
    
    /* PLAYER RESOURCES */
    // Tracks Ammunition, Health, and Armor uses int and float values
    public int bullets;
    public int bulletsMax;
    public int shells;
    public int shellsMax;
    public float health;
    public float healthMax;
    public float armor;
    public float ArmorMax;
    
    /* MOVEMENT */
    public Rigidbody thisRigidBody; 
    public Camera thisCamera;  
    public float pitch; // the mouse movement up/down
    public float yaw;   // the mouse movement left/right
    public float fpForwardBackward; // input float from  W and S keys
    public float fpStrafe;  // input float from A D keys
    public Vector3 inputVelocity;  // cumulative velocity to move character
    public float velocityModifier;  // velocity multiplied by this number
    float verticalLook; 
    
    /*HITSCAN CODE*/

    public Raycast ray;

    // Initializes values of player resources, game states, and movement
    void Start()
    {
        /**/
        
        
        /* MOVEMENT */
        thisRigidBody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
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
        hasShotgun = false;
        blueArmor = false;
        greenArmor = false;
        activeWeapon = "pistol";
        UI.ActiveWeapon(0); // Sets active weapon on UI to the pistol
    }

    // Standard FPS Movement 
    void Update()
    {
        // Standard FPS Movement Code
        yaw = Input.GetAxis("Mouse X");
        transform.Rotate(0f, yaw, 0f);
        
        pitch = Input.GetAxis("Mouse Y");
        verticalLook += -pitch;
        verticalLook = Mathf.Clamp(verticalLook, -80f, 80f);
        thisCamera.transform.localEulerAngles = new Vector3(verticalLook,0f,0f);        
        
        fpForwardBackward = Input.GetAxis("Vertical");
        fpStrafe = Input.GetAxis("Horizontal");

        inputVelocity = transform.forward * fpForwardBackward;
        inputVelocity += transform.right * fpStrafe;
    }
    
    void FixedUpdate()
    {
        // POOP; unused code for pause and menus
        /*if (paused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UI.CloseMenu();
                game.Unpause();
                paused = false;
            }
        }

        else {
        
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UI.OpenMenu();
                game.Pause();
                paused = true;
            }
        }*/

        // Increments Timer for Reload
        if (reloadTimer > 0.0f)
        {
            reloadTimer -= Time.deltaTime;
        } 
        
        // Input for Firing your Weapon
        if (Input.GetKeyDown(KeyCode.Mouse0) && reloadTimer <= 0.0f)
        {
            // Fires a different weapon based on your active weapon
            if (activeWeapon == "pistol")
            {
                if (bullets > 0) // Checks if you have bullets to fire
                {
                    audio.playClip(0); // Pistol Fire sound effect
                    reloadTimer = .25f; // Sets a short reload timer
                    bullets -= 1; // Increments Bullet Counter
                    ray.HitScanFire();
                    // Animation?
                }
            }
            else if (activeWeapon == "shotgun")
            {
                if (shells > 0)
                {
                    audio.playClip(1); // Shotgun Fire sound effect
                    reloadTimer = 0.8f; // Sets a reload timer
                    shells -= 1; // Increments Bullet Counter
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
            // Animation
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && hasShotgun)
        {
            UI.ActiveWeapon(1); // changes visual for active weapon
            audio.playClip(5);
            activeWeapon = "shotgun";
            // Animation
        }
        
        /* APPLIES MOVEMENT */
        thisRigidBody.velocity = inputVelocity * velocityModifier; 
        
        /* UPDATES UI */
        UI.PlayerUpdateUI(activeWeapon, bullets, bulletsMax, shells, shellsMax, health, healthMax, armor, ArmorMax); 
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
                if (armor < 200)
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
            if (health > 10)
            {
                health -= 10;
            }
            else
            {
                health = 0;
            }
            Debug.Log("You took 10 damage from a projectile");
            audio.playClip(3);
            c.gameObject.SetActive(false);
        }
        else if (c.CompareTag("Enemy"))
        {
            if (health > 10)
            {
                health -= 10;
            }
            else
            {
                health = 0;
            }
            Debug.Log("You took damage from touching an enemy");
            audio.playClip(3);
        }
    }

    public void Damage(float d)
    {
        Debug.Log(d);
        if (health > d)
        {
            health -= d;
        }
        else
        {
            health = 0;
        }
    }
}
