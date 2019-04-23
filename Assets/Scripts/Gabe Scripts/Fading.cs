using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// A simple bit of code that can be placed on an object on the canvas to fade it in or out 
// This script should be attached to any popups we want to use in the game

public class Fading : MonoBehaviour
{
    /* UI ELEMENT REFERENCES */
    public Image image;
    public TextMeshProUGUI text;
    
    /* FADE STATE VARIABLES */
    public bool fadingOut; // if true, UI element has falling Alpha
    public bool fadingIn; // if true, UI element has rising Alpha
    private float alpha; // stores the current alpha
    private float fadeSpeed; // stores the fade speed
    
    // Called when the Script is initialized
    void Start()
    {
        image = GetComponent<Image>(); // Grabs the image this script is attached to
        
        /* INITIAL VALUES */
        alpha = 0;
        fadeSpeed = .005f;
        fadingOut = false;
        fadingIn = false;
        
        // Sets the Alpha of the Image (and Text) to 0, making it invisible
        setAlpha(alpha); // Sets the Alpha of the Image (and Text) to 0, making it invisible
    }

    // Changes Alpha of UI Elements if "fadingOut" or "fadingIn" is true
    void FixedUpdate()
    {
        if (fadingOut)
        {
            alpha -= fadeSpeed; // decreases the Alpha
            setAlpha(alpha); // Adjust UI Elements
            
            // Checks if Alpha is at Minimum, ending the Fade Out
            if (alpha <= 0.0f)
            {
                fadingOut = false;
            }
        }

        if (fadingIn)
        {
            alpha += fadeSpeed * 10; // Increases the Alpha. The 10x multiplier is for game fee;
            setAlpha(alpha); // Adjust UI Elements
            
            // Checks if Alpha is at Maximum
            if (alpha >= 1.0f)
            {
                fadingIn = false;
            }
        }
    }

    
    // Triggers the UI Element to fade in or Out
    public void fadeOut()
    {
        fadingIn = false;
        fadingOut = true;
    }
    public void fadeIn()
    {
        fadingOut = false;
        fadingIn = true;
        alpha = 0;
    }
    
    
    /* SETTERS */
    public void setFadeSpeed(float speed)
    {
        fadeSpeed = speed;
    }
    public void setAlpha(float a)
    {
        alpha = a;
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }
}
