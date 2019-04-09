using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class TestPlayerController : MonoBehaviour
{
    
    /* REFERENCES */
    // public GameController game;
    public TestAudioController audio;
    public TestUIController UI;
    
    /* GAMESTATE */
    public bool hasShotgun;
    // public bool paused;
    public string activeWeapon;
    public bool blueArmor;
    public bool greenArmor;
    public float reloadTimer;
    
    /* PLAYER RESOURCES */
    public int bullets;
    public int bulletsMax;
    public int shells;
    public int shellsMax;
    public float health;
    public float healthMax;
    public float armor;
    public float ArmorMax;
    
    /* Movement Variables */
    public Rigidbody thisRigidBody; // the rigidbody we'll be moving
    public Camera thisCamera;   // the camera
    public float pitch; // the mouse movement up/down
    public float yaw;   // the mouse movement left/right
    public float fpForwardBackward; // input float from  W and S keys
    public float fpStrafe;  // input float from A D keys
    public Vector3 inputVelocity;  // cumulative velocity to move character
    public float velocityModifier;  // velocity of conroller multiplied by this number
    float verticalLook = 0f; 

    void Start()
    {
        thisRigidBody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        health = 100;
        healthMax = 100;
        armor = 0;
        ArmorMax = 100;
        bullets = 100;
        bulletsMax = 200;
        shells = 10;
        shellsMax = 50;
        
        hasShotgun = false;
        blueArmor = false;
        greenArmor = false;
        activeWeapon = "pistol";
        UI.ActiveWeapon(0);
    }

    void Update()
    {
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

        reloadTimer -= Time.deltaTime;
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
        }

        else {
            // Movement and Camera Control

       

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UI.OpenMenu();
                game.Pause();
                paused = true;
            }
        }*/

        if (reloadTimer > 0.0f)
        {
            reloadTimer -= Time.deltaTime;
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && reloadTimer <= 0.0f)
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
            UI.ActiveWeapon(0);
            audio.playClip(4);
            activeWeapon = "pistol";
            // Animation
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UI.ActiveWeapon(1);
            audio.playClip(5);
            activeWeapon = "shotgun";
            // Animation
        }
        
        thisRigidBody.velocity = inputVelocity * velocityModifier; // Movement
        
        UI.PlayerUpdateUI(activeWeapon, bullets, bulletsMax, shells, shellsMax, health, healthMax, armor, ArmorMax);
    }
    
    /* PICKUP */
    public void OnTriggerEnter(Collider c)
    {
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
                print("picked up Ammo Clip");
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
                print("picked up Bullet Box");
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
                print("picked up Shells");
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
                print("picked up Shell Box");
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
                print("picked up Medikit");
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
                print("picked up Stimpack");
                audio.playClip(2);
                c.gameObject.SetActive(false);
            }
            
            else if (c.transform.name == "Armor Bonus")
            {
                if (armor < 200)
                {
                    armor += 1;
                }
                print("picked up Armor Bonus");
                audio.playClip(2);
                c.gameObject.SetActive(false);
            }
            
            else if (c.transform.name == "Health Bonus")
            {
                if (health < 200)
                {
                    health += 1;
                }
                print("picked up Health Bonus");
                audio.playClip(2);
                c.gameObject.SetActive(false);
            }
            
            else if (c.transform.name == "Blue Armor")
            {
                if (armor < 200)
                {
                    armor = 200;
                    blueArmor = true;
                    print("picked up Blue Armor");
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
                    print("picked up Green Armor");
                    audio.playClip(2);
                    c.gameObject.SetActive(false);
                }
            }
            
            else if (c.transform.name == "Shotgun")
            {
                hasShotgun = true;
                print("picked up Shotgun");
                UI.popUpMessage();
                audio.playClip(1);
                c.gameObject.SetActive(false);
            }
        }
        
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
            audio.playClip(3);
        }
    }
    
    /* WEAPONS */
    public void FirePistol()
    {
        if (bullets > 0)
        {
            audio.playClip(0);
            reloadTimer = .25f;
            bullets -= 1;
            // Animation
        }
    }

    public void FireShotgun()
    {
        if (shells > 0)
        {
            audio.playClip(1);
            reloadTimer = 0.8f;
            shells -= 1;
            // Animation
        }
    }
}
