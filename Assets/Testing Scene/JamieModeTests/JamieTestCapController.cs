using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;

public class JamieTestCapController : MonoBehaviour {

    public float speed = 10.0f;
    private float translation;
    private float straffe;
    private Rigidbody thisrigidbody;
    
    // Use this for initialization
    void Start () {
        // turn off the cursor
        Cursor.lockState = CursorLockMode.Locked;
        thisrigidbody = GetComponent<Rigidbody>();
    }
	
    // Update is called once per frame
    void Update () {
        // Input.GetAxis() is used to get the user's input
        // You can furthor set it on Unity. (Edit, Project Settings, Input)
        translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        straffe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(straffe, 0, translation);

        if (Input.GetKeyDown("escape")) {
            // turn on the cursor
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
        {
            thisrigidbody.drag = 0;
        }
        else
        {
            thisrigidbody.drag = 5;
        }
    }
}