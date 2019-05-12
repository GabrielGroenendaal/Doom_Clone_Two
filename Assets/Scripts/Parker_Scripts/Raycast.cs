using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Original Script (Raycast) By: Logan
//Editied for Enemy Code By: Parker
//NEEDS TO BE ATTACTED TO THE CAMERA TO WORK
public class Raycast : MonoBehaviour
{
    public float hitScanRange = 100f;
    public float doorRange = 3f;
    public float damage = 5;
    void Start()
    {
        
    }

    void Update()
    {

    }

    //Is called to fire a raycast at a certain distance to open the door
    public void DoorFire()
    {
        RaycastHit hit;

         if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, doorRange) && hit.collider.tag == "Door")
         {
            //Debug.Log("I touched " + hit.collider.gameObject.name);
            //animates door opening
            hit.collider.gameObject.GetComponent<Door>().OpenDoor();
         }
        
    }

    //Is called to fire a raycast at a certain distance can be a changed according to the gun 
    public void HitScanFire()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, hitScanRange) && hit.collider.tag == "Enemy")
        {
            Debug.Log("BANG!");
            //assigns damage to hit enemy
            hit.collider.gameObject.GetComponent<EnemyBaseScript>().EnemyHit(damage);
        }
    }
}
