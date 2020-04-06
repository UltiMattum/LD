using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    private float Damage = 12f;
    private float Range = 100;

    public Camera fpsCAM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
