using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Fading : MonoBehaviour
{
    public bool fading;
    public bool fadingIn;
    public Image image;
    public float alpha;
    public TextMeshProUGUI text;
    
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = 0;
        image.color = tempColor;

        var tempColor2 = text.color;
        tempColor2.a = 0;
        text.color = tempColor2;
        
        alpha = image.color.a;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        alpha = image.color.a;
        
        if (fading)
        {
            alpha -= 0.005f;
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            
            if (alpha <= 0.0f)
            {
                fading = false;
            }
        }

        if (fadingIn)
        {
            alpha += 0.05f;
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            
            if (alpha >= 1.0f)
            {
                fadingIn = false;
            }
        }
    }


    public void fadeOut()
    {
        fadingIn = false;
        fading = true;
    }

    public void fadeIn()
    {
        fading = false;
        fadingIn = true;
        alpha = 0;
    }
}
