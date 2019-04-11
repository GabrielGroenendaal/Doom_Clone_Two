using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Created By:Parker
//**Used By Raycast**
public class EnemyBaseScript : MonoBehaviour
{
    public float health = 5;
    public AudioSource sfxPlayer;
    public AudioClip sfx;
    void Start()
    {
        //adds enemy tag just in case
        transform.gameObject.tag = "Enemy";
        //adds Audio Source just in case
        sfxPlayer = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        //checks health to kill
        if (health < 0)
        {
            Die();
        }
    }

    //assigns damage to object and the enemy reacts with a sound
    public void EnemyHit(float damage)
    {
        health -= damage;
        Debug.Log("Oof");
        sfxPlayer.PlayOneShot(sfx);
    }

    public void Die()
    {
        //change
        //only destroy object if it is in the scene
        //possibly deactivate
        //or set to a corpse 
        Destroy(transform.gameObject);
    }

    public float getHealth()
    {
        return health;
    }

    public void setHealth(float h)
    {
        health = h;
    }
}
