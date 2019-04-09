using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// Very simple script to animate the pickups to bob softly up and down in the air
public class Floating : MonoBehaviour
{
    float originalY; // Stores the original Y position

    public float floatStrength = .1f; // Change the severity of the altitude fluctuation

    // Sets initial values 
    void Start()
    {
        floatStrength = .1f;
        originalY = transform.position.y;
    }
    
    // Transforms the Y position on a sin curve, bobbing up and down
    void Update()
    {
        transform.position = new Vector3(transform.position.x,
            originalY + ((float)Math.Sin(Time.time * 2.5) * floatStrength),
            transform.position.z);
    }
}
