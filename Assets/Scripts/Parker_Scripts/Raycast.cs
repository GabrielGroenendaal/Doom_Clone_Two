﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Original Script (Raycast) By: Logan
//Editied for Enemy Code By: Parker
public class Raycast : MonoBehaviour
{
    public float hitScanRange = 100f;
    public float doorRange = 3f;
    public float damage = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward * hitScanRange, Color.magenta);

        RaycastHit hit;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, doorRange) && hit.collider.tag == "Door")
            {
                Debug.Log("I touched " + hit.collider.gameObject.name);
                //animates door opening
                hit.collider.gameObject.GetComponent<Door>().OpenDoor();
            }
        }


         if (Input.GetMouseButton(0))
         {
             if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, hitScanRange) && hit.collider.tag == "Enemy")
             {
                 Debug.Log("BANG!");
                 //assigns damage to hit enemy
                 hit.collider.gameObject.GetComponent<EnemyBaseScript>().EnemyHit(damage);
             }
         }
    }
}
