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

public class TestUIController : MonoBehaviour
{
    /* SCRIPT REFERENCES */
    public TestPlayerController player;
    public Fading popUP;
    public GameController game;

    /* GAMESTATE VARIABLES */
    public string UIState; // Indicates if UI is menu, game, gameOver, etc. Not functional.
    public bool fadeOut; // a boolean that works with the timer to create a fluid fade-in-then-out
    public float timer;

    /* GAME UI ELEMENTS */
    public Image GameUI; 
    public RawImage Shotgun;
    public RawImage Pistol;
    public TextMeshProUGUI Health;
    public TextMeshProUGUI Armor;
    public TextMeshProUGUI Ammo;
    public TextMeshProUGUI Bullets;
    public TextMeshProUGUI Shells;

    // We don't have a menu UI set up yet
    /* MENU UI ELEMENTS */
    /* public Image menuUI;
    public Button button1;
    public Button button2;*/

    // Increments the timer and triggers the FadeOut
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
        Health.text = healthPercent + "%";
        
        // ARMOR
        var armorPercent = (armor / armorMax) * 100; 
        Armor.text = armorPercent + "%";
        
        // AMMO
        Bullets.text = bullets + " / " + bulletMax;
        Shells.text = shells + " / " + shellsMax;
        
        // ACTIVE AMMO (changes depending on the equipped weapon)
        if (activeWeapon == "pistol")
        {
            Ammo.text = bullets.ToString();
        }

        else if (activeWeapon == "shotgun")
        {
            Ammo.text = shells.ToString();
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
            Shotgun.color = new Color(Shotgun.color.r, Shotgun.color.g, Shotgun.color.b, 0.0f);
            Pistol.color = new Color(Pistol.color.r, Pistol.color.g, Pistol.color.b, 1.0f);
        }
        else if (i == 1)
        {
            Shotgun.color = new Color(Shotgun.color.r, Shotgun.color.g, Shotgun.color.b, 1.0f);
            Pistol.color = new Color(Pistol.color.r, Pistol.color.g, Pistol.color.b, 0.0f);
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
