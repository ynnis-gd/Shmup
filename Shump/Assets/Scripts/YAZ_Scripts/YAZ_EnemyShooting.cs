using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YAZ_EnemyShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float timeBetweenShots = 1;
    //Bool pour empêcher le spam de projectiles
    private bool hasShot = false;
    //Bool qui empêche le 1er tir pour ne pas tirer dès le début
    private bool firstShotStopped = false;

    public void EnemyShoot()
    {
        if (!hasShot)
        {
            hasShot = true;
            StartCoroutine(ShootCoroutine());
        }
    }
    
    IEnumerator ShootCoroutine()
    {
        if (firstShotStopped)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);      
            yield return new WaitForSeconds(timeBetweenShots);
        }
        else
        {
            firstShotStopped = true;
            yield return new WaitForSeconds(0.5f);
        }
        hasShot = false;
        StopCoroutine(ShootCoroutine());
    }

}
