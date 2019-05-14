using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head_Bobbing_Script : MonoBehaviour
{
    public Animator head_bobbing;

    // Start is called before the first frame update
    void Start()
    {
        head_bobbing.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            head_bobbing.speed += 0.2f;
            if(head_bobbing.speed > 1f)
            {
                head_bobbing.speed = 1f;
            }
        }
        else
        {
            head_bobbing.speed = 0f;
        }
    }
}
