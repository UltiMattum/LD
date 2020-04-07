using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    private float Damage = 12f;
    private float Range = 100;
    public Camera fpsCAM;

    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }

     void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(fpsCAM.transform.position, fpsCAM.transform.forward, out hit, Range))
        {
            Debug.Log(hit.transform.name);
        }
        
    }
}
