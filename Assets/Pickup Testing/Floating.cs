using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Floating : MonoBehaviour
{
    float originalY;

    public float floatStrength = .1f; // You can change this in the Unity Editor to 
    // change the range of y positions that are possible.

    void Start()
    {
        floatStrength = .1f;
        originalY = transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x,
            originalY + ((float)Math.Sin(Time.time * 2.5) * floatStrength),
            transform.position.z);
    }
}
