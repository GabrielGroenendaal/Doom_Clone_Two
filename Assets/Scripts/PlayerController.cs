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
    public bool hasShotgun;
    public bool paused = false;
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
    

    /* MOVEMENT VARIABLES */
    Rigidbody thisRigidBody;
    public float moveSpeed;
    public float fpForward;
    public float fpStrafe;
    public Vector3 inputVector;
    public Vector3 outputVector;

    
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
        
        thisRigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        /* if (paused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UI.CloseMenu();
                game.Unpause();
                paused = false;
            }
        }*/

      
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
                audio.playClip(4);
                activeWeapon = "pistol";
                // Animation
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                UI.ActiveWeapon(2);
                audio.playClip(5);
                activeWeapon = "shotgun";
                // Animation
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UI.OpenMenu();
                game.Pause();
                paused = true;
            }
        
        
        UI.PlayerUpdateUI(bullets, bulletsMax, shells, shellsMax, health, healthMax, armor, ArmorMax);
        
        // MOVEMENT PARKER
        thisRigidBody.velocity = inputVector * moveSpeed + (Physics.gravity * 0.69f);

        outputVector = thisRigidBody.velocity;
    }
    
    void Update()
    {
        //MOVEMENT  PARKER
        float pitch = Input.GetAxis("Mouse X");
        float yaw = Input.GetAxis("Mouse Y");

        this.transform.Rotate(0, pitch, 0);         // effects camera pitch
        Camera.main.transform.Rotate(-yaw, 0, 0);    // effect camera yaw

        fpForward = Input.GetAxis("Vertical");
        fpStrafe = Input.GetAxis("Horizontal");
    
        inputVector = transform.forward * fpForward;
        inputVector += transform.right * fpStrafe;
    }
    
    /* PICKUP */
    public void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("pickup"))
        {
            if (c.transform.name == "Ammo Clips")
            {
                if (bullets < 200)
                {
                    bullets += 10;
                    c.gameObject.SetActive(false);
                }
                
            }
            
            else if (c.transform.name == "Bullet boxes")
            {
                if (bullets < 200)
                {
                    bullets += 50;
                    c.gameObject.SetActive(false);
                }
            }
            
            else if (c.transform.name == "Shells")
            {
                if (shells < 50)
                {
                    shells += 4;
                    c.gameObject.SetActive(false);
                }
            }
            
            else if (c.transform.name == "Shell boxes")
            {
                if (shells < 50)
                {
                    shells += 20;
                    c.gameObject.SetActive(false);
                }
            }
            
            else if (c.transform.name == "Medikits")
            {
                
            }
            
            else if (c.transform.name == "Stimpacks")
            {
                
            }
            
            else if (c.transform.name == "Armor bonuses")
            {
                
            }
            
            else if (c.transform.name == "Health Bonuses")
            {
                
            }
            
            else if (c.transform.name == "Blue Armor")
            {
                
            }
            
            else if (c.transform.name == "Green Armor")
            {
                
            }
        }
        
        else if (c.CompareTag("projectile"))
        {
            
        }
        
        else if (c.CompareTag("enemy"))
        {
            
        }
    }
    
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
