using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestUIController : MonoBehaviour
{
    /* REFERENCES */
    public TestPlayerController player;
    public Fading popUP;
    // public GameController game;

    /* GAMESTATE */
    // public string UIState;
    public bool fadeOut;
    public float timer;

    /* GAME UI ELEMENTS */
    // public Image GameUI;
    public RawImage Shotgun;
    public RawImage Pistol;
    public TextMeshProUGUI Health;
    public TextMeshProUGUI Armor;
    public TextMeshProUGUI Ammo;
    public TextMeshProUGUI Bullets;
    public TextMeshProUGUI Shells;

    /* MENU UI ELEMENTS */
    /* public Image menuUI;
    public Button button1;
    public Button button2;*/

    void Start()
    {
 
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
    public void PlayerUpdateUI(string activeWeapon, int bullets, int bulletMax, int shells, int shellsMax, float health, float healthMax,
        float armor, float armorMax)
    {
        var healthPercent = (health / healthMax) * 100;
        Health.text = healthPercent + "%";

        var armorPercent = (armor / armorMax) * 100;
        Armor.text = armorPercent + "%";

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
    
    // This Code just Switches Around the Art for your weapon
    public void ActiveWeapon(int i)
    {
        if (i == 0)
        {
            var tempColor = Shotgun.color;
            tempColor.a = 0;
            Shotgun.color = tempColor;
          
            var tempColor1 = Pistol.color;
            tempColor1.a = 1;
            Pistol.color = tempColor1;
        }
        else if (i == 1)
        {
            // Shotgun.Active();
            // Pistol.Inactive();
            var tempColor = Shotgun.color;
            tempColor.a = 1;
            Shotgun.color = tempColor;
          
            var tempColor1 = Pistol.color;
            tempColor1.a = 0;
            Pistol.color = tempColor1;
        }
    }

    public void popUpMessage()
    {
        popUP.fadeIn();
        fadeOut = true;
        timer = 2.2f;
    }
}
