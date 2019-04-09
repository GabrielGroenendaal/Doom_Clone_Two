using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Created By: Parker
//Used with Code from (Raycast_orignal) By: Logan
//**Used By Raycast**
public class Door : MonoBehaviour
{
    public float openSpeed = 30f;
    
    // Start is called before the first frame update
    void Start()
    {
        //adds door tag just in case
        transform.gameObject.tag = "Door";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        transform.Translate(Vector3.up * openSpeed * Time.deltaTime);
    }
}
