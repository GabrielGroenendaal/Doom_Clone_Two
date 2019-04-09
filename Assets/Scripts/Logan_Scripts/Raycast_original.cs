using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast_original : MonoBehaviour
{
    public float cameraRayDistance = 100f;
    public float playerPos;
    public float openSpeed = 30f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward * cameraRayDistance, Color.magenta);

        RaycastHit hit;

        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, cameraRayDistance))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("I touched " + hit.collider.gameObject.name);
                playerPos = Vector3.Distance(this.transform.position, hit.collider.gameObject.transform.position);

                if(playerPos < 3 && hit.collider.tag == "Door")
                {
                    hit.collider.gameObject.transform.Translate(Vector3.up * openSpeed * Time.deltaTime);
                }
            }

            if (Input.GetMouseButton(0))
            {
                Debug.Log("BANG!");
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
