using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRaycast : MonoBehaviour
{    
    /* ENEMY INFO */
    public float health = 10;
    public float damage = 5;
    public float range = 50f;
    /* REFERENCES */
    public EnemyBaseScript enemyScript;
    public GameObject player;
    public Collider playerCol;
    public PlayerController playerScript;
    public GameObject projectile;
    public Animator thisAnimator;
    
    //debug booleans
    public bool debug = false;
    void Start()
    {
        enemyScript.setHealth(health);
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerController>();
        playerCol = player.GetComponent<Collider>();
        
    }

    void Update()
    {
        
    }
    public void fireScan(float i)
    {
        RaycastHit hit;
        Debug.Log("Enemy Fired");
        Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.forward)*range, Color.yellow);
        
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, range) && hit.collider.tag == "Player")
        {
            Debug.Log("Player Hit");
            //enemyScript.sound();
            playerScript.Damage(i);
        }
    }


}
