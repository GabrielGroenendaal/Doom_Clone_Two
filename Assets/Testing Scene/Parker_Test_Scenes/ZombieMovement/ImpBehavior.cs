using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ImpBehavior : MonoBehaviour
{
    /* ENEMY INFO */
    public float health = 20;
    public float damage = 10;
    public float sightRange = 50;
    public float playerTargetRange = 5;
    public float MeleeRange = 2;
    public float speed = 1.4f;
    public float wallDetectionRange = 1f;
    public float range = 50f;

    public Boolean isWalking;
    
    public bool noFloor = false;
    
    //timer increased when enemy is moving
    public float walkingTimer;
   
    //when walkingTimer equals fireTime the Imp will attack
    public float fireTime = 2;
    public float fireWait = 3f;
    public bool shot = false;
    
    public float wallColTimer = 7;
    public float turnTime = 1;
    public bool turned = false;
    public bool turned2 = false;
    public bool fart = false;
    public bool t = false;    
    
    //time the enemy ignores a player after hitting a wall
    public float ignoreTime = 2;

    public float wallTime = 1;
    
    /* REFERENCES */
    public EnemyBaseScript enemyScript;
    public GameObject player;
    public PlayerController playerScript;
    public GameObject projectile;
    public Animator thisAnimator;
    
    //debug booleans
    public Boolean debug = false;
    void Start()
    {
        enemyScript = gameObject.GetComponent<EnemyBaseScript>();
        gameObject.name = "Imp";
        enemyScript.setHealth(health);
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerController>();
        thisAnimator = transform.Find("body").GetComponent<Animator>();
        transform.LookAt(PlayerPos());
    }
    
    void Update()
    {
        //Debug.Log("g");
        WalkingTimerTick();
        if (walkingTimer < fireTime)
        {
            Walk();
        }
        else
        {
            Fire();
        }
    }

    void WalkingTimerTick()
    {
        walkingTimer += Time.deltaTime;
    }

    void WalkingTimerReset()
    {
        walkingTimer = 0;
    }
    
    void WallColTimerTick()
    {
        wallColTimer += Time.deltaTime;
    }

    void WallColTimerReset()
    {
        wallColTimer = 0;
    }

    public void Fire()
    {
        if (debug)
        {
            fart = true;
            
        }
        float distance = Vector3.Distance(PlayerPos(), transform.position);

        if (distance < sightRange)
        {
            if (debug)
            {
                t = true;
            }
            thisAnimator.SetTrigger("Throw");
            thisAnimator.ResetTrigger("Walking");
        }

        if (!shot && walkingTimer > fireTime && walkingTimer < fireTime + (fireWait * 1/7))
        {
            if (distance < MeleeRange)
            {
                transform.LookAt(PlayerPos());
                //MeleeAttack();
                //thisAnimator.SetTrigger("Throw");
                //transform.Rotate(Vector3.up, 100);
                if (debug)
                {
                    Debug.Log("Melee Attack");
                }
            }
            else if (distance < sightRange)
            {
                transform.LookAt(PlayerPos());
                
                if (debug)
                {
                    Debug.Log("Turn");
                }
            }
            else
            {
                //transform.Rotate(Vector3.up,180);
            }
        }else if (!shot && walkingTimer > fireTime + ((fireWait*2)/3))
        {
            if (distance < sightRange)
            {
                //FireScan();
                FireProjectile();
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
            if (debug)
            {
                t = false;
                fart = false;
            }
            WalkingTimerReset();
            
        }
    }

    
    public void Walk()
    {
        WallCol();
        thisAnimator.SetTrigger("Walking");
        thisAnimator.ResetTrigger("Throw");
        float distance = Vector3.Distance(playerPos(), transform.position);
        if (distance < sightRange)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }
    
    //factors wall contact into AI pathing
    public void WallCol()
    {
        WallColTimerTick();
        float distance = Vector3.Distance(PlayerPos(), transform.position);
        RaycastHit hit;
        if (debug)
        {
            
            Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.forward), Color.green);
            //Debug.DrawRay(this.transform.position, new Vector3(transform.position.x,transform.position.y + 1,transform.position.z + 1), Color.red);
            Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.back), Color.magenta);
            Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.right), Color.magenta);
            Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.left), Color.magenta);
        }
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, -0.9f, 2)), out hit, wallDetectionRange*2))
        {
            Debug.DrawRay(this.transform.position, transform.TransformDirection(new Vector3(0,-0.9f,2)) * wallDetectionRange, Color.cyan);

            if (debug)
            {
                //Debug.Log("i" + hit.collider.name);
            }

            if (hit.collider.Equals(null))
            {
                noFloor = true;
            }
            else
            {
                noFloor = false;
            }
        }
        else
        {
            noFloor = true;
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,wallDetectionRange) )
        {
            
            //transform.LookAt(hit.transform);
            transform.Rotate(Vector3.up, 120);
            WallColTimerReset();
            if (debug)
            {
                Debug.Log("F");
            }

        }
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit,wallDetectionRange))
        {
            
            //transform.LookAt(hit.transform);
            //transform.Rotate(Vector3.up, 180);
            //WallColTimerReset();
            if (debug)
            {
                Debug.Log("B");
            }

        }
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit,wallDetectionRange))
        {
            
            //transform.LookAt(hit.transform);
            //transform.Rotate(Vector3.up, 90);
            //WallColTimerReset();
            if (debug)
            {
                Debug.Log("L");
            }

        }
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit,wallDetectionRange))
        {

            //transform.LookAt(hit.transform);
            //transform.Rotate(Vector3.up, -90);
            //WallColTimerReset();
            if (debug)
            {
                Debug.Log("R");
            }
        }
        else if (distance < playerTargetRange)
        {
            transform.LookAt(PlayerPos());
        }
        else if (distance < MeleeRange) 
        {
            transform.LookAt(PlayerPos());
            MeleeAttack();
            if (debug)
            {
                Debug.Log("attacking player");
            }

            WallColTimerReset();
        }
        else if (wallColTimer > (turnTime * 3))
        {
            WallColTimerReset();
            transform.LookAt(PlayerPos());
            transform.Rotate(Vector3.up, -60);
            turned = false;
            turned2 = false;
        }
        else if (wallColTimer > (turnTime * 2))
        {
            //WallColTimerReset();
            if (!turned2)
            {
                transform.Rotate(Vector3.up, -120);
                turned2 = true;
            }
        }
        else if (wallColTimer > turnTime)
        {
            if (!turned) 
            {
                transform.Rotate(Vector3.up, 60);
                turned = true;
            }
        }

    }
    public Vector3 PlayerPos()
    {
        Vector3 newpos = new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z );
        return newpos;
    }
          
    public void FireScan()
    {
        RaycastHit hit;
        if (debug)
        {
            Debug.Log("Enemy Fired");
        }

        Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.forward)*range, Color.yellow);
        
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, range) && hit.collider.CompareTag("Player"))
        {
            if (debug)
            {
                Debug.Log("Player Hit");
            }

            enemyScript.sound();
            playerScript.Damage(damage);
        }
    }

    public void MeleeAttack()
    {
        enemyScript.sound();
        playerScript.Damage(damage);
    }

    public void FireProjectile()
    {
        Vector3 f = Vector3.forward;
        Vector3 bop = transform.position + transform.TransformDirection(Vector3.forward);
        bop = new Vector3(bop.x, bop.y + 1, bop.z);
        
        GameObject projectileShot = Instantiate(projectile, bop,Quaternion.identity);
        projectileShot.transform.LookAt(player.transform);
        if (debug)
        {
            Debug.Log("Enemy Fired");
        }
    }
    
    /*
    public void TimerReset(float timer)
    {
        timer = 0;
    }

    public void TimerTick(float timer)
    {
        timer += Time.deltaTime;
    }

    public void TimerTick(float timer, float rate)
    {
        timer += Time.deltaTime * rate;
    }
    */

    public float WalkingTimer
    {
        get => walkingTimer;
        set => walkingTimer = value;
    }
    
    public Vector3 playerPos()
    {
        Vector3 newpos = new Vector3(player.transform.position.x,transform.position.y, player.transform.position.z );
        return newpos;
    }
}
