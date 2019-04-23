using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiemenBehavior : MonoBehaviour
{
    //enemy info
    public float health = 20;
    public float damage = 10;
    public float sightRange = 50;
    public float tooCloseRange = 15;
    public float speed = 1.4f;
    public float wallDetectionRange = 1f;
    public float range = 50f;

    public Boolean isWalking;
    
    //timer increased when enemy is moving
    public float walkingTimer;
   
    //when walkingTimer equals fireTime the Zombieman will attack
    public float fireTime = 5;
    public float fireWait = 0.5f;
    public bool shot = false;
    
    public float wallColTimer = 7;
    
    //time the enemy ignores a player after hitting a wall
    public float ignoreTime = 2;

    public float wallTime = 1;
    
    public EnemyBaseScript enemyScript;
    public GameObject player;
    public TestPlayerController playerScript;
    
    //debug booleans
    public Boolean debug = false;
    void Start()
    {
        enemyScript = gameObject.GetComponent<EnemyBaseScript>();
        gameObject.name = "Zombieman";
        enemyScript.setHealth(health);
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<TestPlayerController>();
    }
    
    void Update()
    {
        //Debug.Log("g");
        walkingTimerTick();
        if (walkingTimer < fireTime)
        {
            walk();
        }
        else
        {
            fire();
        }
    }

    void walkingTimerTick()
    {
        walkingTimer += Time.deltaTime;
    }

    void walkingTimerReset()
    {
        walkingTimer = 0;
    }
    
    void wallColTimerTick()
    {
        wallColTimer += Time.deltaTime;
    }

    void wallColTimerReset()
    {
        wallColTimer = 0;
    }

    public void fire()
    {
        float distance = Vector3.Distance(playerPos(), transform.position);

        if (!shot && walkingTimer > fireTime && walkingTimer < fireTime + (fireWait/7))
        {
            if (distance < tooCloseRange)
            {
                transform.LookAt(playerPos());
                transform.Rotate(Vector3.up, 100);
                if (debug)
                {
                    Debug.Log("Too Close");
                }
            }
            else if (distance < sightRange)
            {
                transform.LookAt(playerPos());
                Debug.Log("Turn");
                if (debug)
                {

                }
            }
            else
            {
                //transform.Rotate(Vector3.up,180);
            }
        }else if (!shot && walkingTimer > fireTime + ((fireWait*2)/7))
        {
            if (distance < sightRange)
            {
                FireScan();
                shot = true;
                if (debug)
                {
                    Debug.Log("Fire");
                }
            }
            else
            {
                //transform.Rotate(Vector3.up,180);
            }
        }

        if (walkingTimer > fireTime + fireWait)
        {
            //FIRES BULLET
            shot = false;
            walkingTimerReset();
            
        }
    }

    
    public void walk()
    {
        wallCol();
        transform.Translate(Vector3.forward*Time.deltaTime*speed);
    }
    
    //factors wall contact into AI pathing
    public void wallCol()
    {
        wallColTimerTick();
        float distance = Vector3.Distance(playerPos(), transform.position);
        RaycastHit hit;
        if (debug)
        {
            
            Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.forward), Color.green);
            //Debug.DrawRay(this.transform.position, new Vector3(transform.position.x,transform.position.y + 1,transform.position.z + 1), Color.red);
            Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.back), Color.magenta);
            Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.right), Color.magenta);
            Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.left), Color.magenta);
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,wallDetectionRange) )
        {
            
            //transform.LookAt(hit.transform);
            transform.Rotate(Vector3.up, 180);
            wallColTimerReset();
            if (debug)
            {
                Debug.Log("F   pppppppppppppp");
            }

        }
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit,wallDetectionRange))
        {
            
            //transform.LookAt(hit.transform);
            //transform.Rotate(Vector3.up, 180);
            wallColTimerReset();
            if (debug)
            {
                Debug.Log("B");
            }

        }
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit,wallDetectionRange))
        {
            
            //transform.LookAt(hit.transform);
            transform.Rotate(Vector3.up, 90);
            wallColTimerReset();
            if (debug)
            {
                Debug.Log("L");
            }

        }
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit,wallDetectionRange))
        {

            //transform.LookAt(hit.transform);
            transform.Rotate(Vector3.up, -90);
            wallColTimerReset();
            if (debug)
            {
                Debug.Log("R");
            }
        }else if (distance < tooCloseRange && wallColTimer > ignoreTime)            
        {
            transform.LookAt(playerPos());
            transform.Rotate(Vector3.up,95);
            if (debug)
            {
                Debug.Log("avoiding player");
            }

            wallColTimerReset();
        }
    }

    public Vector3 playerPos()
    {
        Vector3 newpos = new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z );
        return newpos;
    }
    
        
    public void FireScan()
    {
        RaycastHit hit;
        Debug.Log("Enemy Fired");
        
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, range) && hit.collider.tag == "Player")
        {
            Debug.Log("Player Hit");
            enemyScript.sound();
            playerScript.Damage(damage);
        }
    }
}
