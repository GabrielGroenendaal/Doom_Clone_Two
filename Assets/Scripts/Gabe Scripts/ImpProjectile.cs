using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpProjectile : MonoBehaviour
{
    public float speed; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision c)
    {
        if (!c.gameObject.CompareTag("player") && !c.gameObject.CompareTag("enemy") && !c.gameObject.CompareTag("Pickup"))
        {
            transform.gameObject.SetActive(false);
        }
    }
}
