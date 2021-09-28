using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YAZ_ShipShooting : MonoBehaviour
{
    public Transform firePoint;
    private YAZ_ObjectPooler objectPooler;

    // Update is called once per frame
    void Start()
    {
        objectPooler = YAZ_ObjectPooler.instance;
    }
    
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        objectPooler.SpawnFromPool("PlayerBullet", firePoint.position, firePoint.rotation);
    }
}
