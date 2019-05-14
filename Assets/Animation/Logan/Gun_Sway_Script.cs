using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Sway_Script : MonoBehaviour
{
    public Animator gun_sway;

    // Start is called before the first frame update
    void Start()
    {
        gun_sway.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            gun_sway.speed += 0.2f;
            if (gun_sway.speed > 1f)
            {
                gun_sway.speed = 1f;
            }
        }
        else
        {
            gun_sway.speed = 0f;
        }
    }
}
