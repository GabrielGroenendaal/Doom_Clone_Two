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
