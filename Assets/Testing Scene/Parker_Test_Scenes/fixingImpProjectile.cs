using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixingImpProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Projectile"))
        {
            //c.gameObject.SetActive(false);
        }
    }
}
