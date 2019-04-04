using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// This script is for managing the UI

public class UIControl : MonoBehaviour
{
    /* REFERENCES */
    public PlayerController player;
    public GameController game;

    /* GAMESTATE */
    public string UIState;

    /* GAME UI ELEMENTS */
    public Image GameUI;
    public Image Shotgun;
    public Image Pistol;
    public TextMeshPro Health;
    public TextMeshPro Armor;
    public TextMeshPro Ammo;
    public TextMeshPro Bullets;
    public TextMeshPro Shells;
    public TextMeshPro Rockets;
    public TextMeshPro Cells;

    /* MENU UI ELEMENTS */
    public Image menuUI;
    public Button button1;
    public Button button2;

    void Start()
    {
 
    }


    void Update()
    {

    }

    /* PLAYER UPDATE */
    public void PlayerUpdateUI(int bullets, int bulletMax, int shells, int shellsMax, float health, float healthMax,
        float armor, float armorMax)
    {
        var healthPercent = (health / healthMax) * 100;
        Health.text = healthPercent.ToString();

        var armorPercent = (armor / armorMax) * 100;
        Armor.text = armorPercent.ToString();

        Bullets.text = bullets + " / " + bulletMax;
        Shells.text = shells + " / " + shellsMax;

        if (player.activeWeapon == "pistol")
        {
            Ammo.text = bullets.ToString();
        }

        else if (player.activeWeapon == "shotgun")
        {
            Ammo.text = shells.ToString();
        }
    }

    /* MENU */
    public void OpenMenu()
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
    }

    public void ActiveWeapon(int i)
    {
        if (i == 1)
        {
            // Pistol.Active();
            // Shotgun.Inactive();
        }
        else if (i == 2)
        {
            // Shotgun.Active();
            // Pistol.Inactive();
        }
    }
}
