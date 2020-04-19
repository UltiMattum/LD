using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDur;

    void Update()
    {
        DestroyBullet();
    }

    public void DestroyBullet()
    {
        bulletDur -= Time.deltaTime;
        if (bulletDur < 0)
        {
            // Destroy this gameObject
            Destroy(gameObject);
        }
    }
}
