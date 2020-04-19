using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    // private float Range = 100;
    public Camera fpsCAM;
    public Transform Gunsform;
    public Rigidbody bullet;
    public float bulletSpeed;

    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }

     void Shoot()
    {
        Rigidbody bulletFired;
        // RaycastHit hit;
        /*
        if(Physics.Raycast(fpsCAM.transform.position, fpsCAM.transform.forward, out hit, Range))
        {
            Debug.Log(hit.transform.name);
        }
        */
        bulletFired = Instantiate(bullet, Gunsform.position, Gunsform.rotation);
        bulletFired.velocity = transform.TransformDirection(Vector3.forward*bulletSpeed);
        
    }
}
