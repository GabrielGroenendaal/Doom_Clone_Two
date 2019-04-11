using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiemenBehavior : MonoBehaviour
{
    //enemy info
    public float health = 20;
    public float damage;
    public float sightRange = 100;
    public float tooCloseRange = 5;


    public Boolean isWalking;
    
    //timer increased when enemy is moving
    public float walkingTimer;
    
    //when walkingTimer equals fireTime the Zombieman will attack
    public float fireTime; 
    
    public EnemyBaseScript enemyScript;
    public GameObject player;
    
    void Start()
    {
        enemyScript = gameObject.GetComponent<EnemyBaseScript>();
        gameObject.name = "Zombieman";
        enemyScript.setHealth(health);
        player = GameObject.Find("Player");
    }
    
    void Update()
    {
        walkingTimerTick();
    }

    void walkingTimerTick()
    {
        walkingTimer += Time.deltaTime;
    }

    void walkingTimerReset()
    {
        walkingTimer = 0;
    }

    public void fire()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        
        if (distance < tooCloseRange)
        {
            transform.Rotate(Vector3.up,120);
        }
        else if (distance < sightRange)
        {
            transform.LookAt(player.transform);
        }
        else
        {
            transform.Rotate(Vector3.up,180);
        }
        //FIRES BULLET
    walkingTimerReset();
    }

    
    public void walk()
    {
        transform.Translate(Vector3.forward);
    }
}
