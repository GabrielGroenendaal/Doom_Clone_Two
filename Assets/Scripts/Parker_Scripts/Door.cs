using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Created By: Parker
//Used with Code from (Raycast_orignal) By: Logan
//**Used By Raycast**
public class Door : MonoBehaviour
{
    public float openSpeed = 0.2f;
    public bool opening = false;
    public float openingTime = 1;
    public float timer = 0;
    public bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        //adds door tag just in case
        transform.gameObject.tag = "Door";
    }

    // Update is called once per frame
    void Update()
    {
        if (opening && !done)
        {
            if ( timer > openingTime)
            {
                opening = false;
                done = true;

            }
            transform.Translate(Vector3.up * openSpeed * Time.deltaTime);
            timer += Time.deltaTime;
        }
    }

    public void OpenDoor()
    {
        opening = true;
    }
}
