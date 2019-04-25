using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// This script manages the UI, updating it with information provided by the PlayerController, including:
//    (A) Ammunition of Active Weapon
//    (B) Total Ammunition
//    (C) Health Percentage
//    (D) Armor Percentage
//    (E) Equipped Weapon

public class UIController : MonoBehaviour
{
    /* SCRIPT REFERENCES */
    public PlayerController player;
    public Fading popUP;
    public GameController game;

    /* GAMESTATE VARIABLES */
    public string UIState; // Indicates if UI is menu, game, gameOver, etc. Not functional.
    public bool fadeOut; // a boolean that works with the timer to create a fluid fade-in-then-out
    public float timer;

    /* GAME UI ELEMENTS */
    //public GameObject Canvas;
    //Currently Empty!
    public Image GameUI; 
    public RawImage ShotgunImage;
    public RawImage PistolImage;
    public TextMeshProUGUI HealthPercentage;
    public TextMeshProUGUI ArmorPercentage;
    public TextMeshProUGUI AmmoNumber;
    public TextMeshProUGUI BulletsAmount;
    public TextMeshProUGUI ShellsAmount;

    // We don't have a menu UI set up yet
    /* MENU UI ELEMENTS */
    /* public Image menuUI;
    public Button button1;
    public Button button2;*/

    // Increments the timer and triggers the FadeOut
    void Start()
    {
        //skipping GameUI
        ShotgunImage = GameObject.Find("Shotgun Image").GetComponent<RawImage>();
        PistolImage = GameObject.Find("Pistol Image").GetComponent<RawImage>();
        HealthPercentage = GameObject.Find("Health Percentage").GetComponent<TextMeshProUGUI>();
        ArmorPercentage = GameObject.Find("Armor Percentage").GetComponent<TextMeshProUGUI>();
        AmmoNumber = GameObject.Find("Ammo Number").GetComponent<TextMeshProUGUI>();
        BulletsAmount = GameObject.Find("Bullet Count").GetComponent<TextMeshProUGUI>();
        ShellsAmount = GameObject.Find("Shell Count").GetComponent<TextMeshProUGUI>();

    }
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0 && fadeOut)
        {
            popUP.fadeOut();
            fadeOut = false;
        }
    }

    /* PLAYER UPDATE */
    // This code takes all the values provided by the TestPlayerController and translates them to the UI Overlay
    
    public void PlayerUpdateUI(string activeWeapon, int bullets, int bulletMax, int shells, int shellsMax, float health, float healthMax,
        float armor, float armorMax)
    {
        // HEALTH
        var healthPercent = (health / healthMax) * 100; 
        HealthPercentage.text = healthPercent + "%";
        
        //ARMORor / armorMax) * 100; 
        var armorPercent = (armor + "%");
        ArmorPercentage.text = armorPercent;

        
        // AMMO
        BulletsAmount.text = bullets + " / " + bulletMax;
        ShellsAmount.text = shells + " / " + shellsMax;
        
        // ACTIVE AMMO (changes depending on the equipped weapon)
        if (activeWeapon == "pistol")
        {
            AmmoNumber.text = bullets.ToString();
        }

        else if (activeWeapon == "shotgun")
        {
            AmmoNumber.text = shells.ToString();
        }
        
    }

    /* MENU */
    /*public void OpenMenu()
    {
        UIState = "menu";
        // GameUI.hide()
        // MenuUI.show()
    }

    public void CloseMenu()
    {
        UIState = "game";
        // GameUI.show();
        // MenuUI.hide();  
    }*/
    
    // This Code just Switches Around the Art for your "active" weapon
    public void ActiveWeapon(int i)
    {
        if (i == 0)
        {
            ShotgunImage.color = new Color(ShotgunImage.color.r, ShotgunImage.color.g, ShotgunImage.color.b, 0.0f);
            PistolImage.color = new Color(PistolImage.color.r, PistolImage.color.g, PistolImage.color.b, 1.0f);
        }
        else if (i == 1)
        {
            ShotgunImage.color = new Color(ShotgunImage.color.r, ShotgunImage.color.g, ShotgunImage.color.b, 1.0f);
            PistolImage.color = new Color(PistolImage.color.r, PistolImage.color.g, PistolImage.color.b, 0.0f);
        }
    }
    
    // Triggers a PopUp Message, which in this case is the "Shotgun Found" message.
    public void popUpMessage()
    {
        popUP.fadeIn();
        fadeOut = true;
        timer = 2.2f;
    }
}
