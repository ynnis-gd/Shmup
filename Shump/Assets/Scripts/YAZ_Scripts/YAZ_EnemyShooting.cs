using System;
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

    [Range(1, 10)] public int numberOfProjectiles = 1;
    [Range(-360, 360)]public int initialRadius = 0;
    [Range(-360, 360)]public int addedRadius = 0;
    //Variables qui décide de la rotation de base des projectile (1ère pour décider de direction initiale, 2e pour calibrer)
    
    [Range(0, 360)] public float radiusOfPattern = 360f;


    private Vector2 startPoint;
    private const float radius = 1f;
    private float bulletSpeed;
    
    private YAZ_ObjectPooler objectPooler;

    private void Awake()
    {
        bulletSpeed = bulletPrefab.GetComponent<YAZ_EnemyBullet>().speed;
        objectPooler = YAZ_ObjectPooler.instance;
    }

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
            startPoint = transform.position;
            
            yield return BulletShot(numberOfProjectiles);
        }
        else
        {
            firstShotStopped = true;
            yield return new WaitForSeconds(0.5f);
        }
        hasShot = false;
        StopCoroutine(ShootCoroutine());
    }

    private object BulletShot(int bulletNumber)
    {
        if (bulletNumber == 1)
        {
            numberOfProjectiles++;
        }

        float angleStep = radiusOfPattern / (numberOfProjectiles - 1);
        float angle = 0f;

        if (numberOfProjectiles == 2 && bulletNumber == 1)
        {
            numberOfProjectiles--;
        }

        for (int i = 0; i <= bulletNumber - 1; i++)
        {
            //Calcule la direction des projectiles

            float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180 + ((initialRadius + addedRadius) * Mathf.PI) / 180) * radius;
            float projectileDirYPosition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180 + ((initialRadius + addedRadius) * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2(projectileDirXPosition, projectileDirYPosition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * bulletSpeed;
            GameObject tempObject = objectPooler.SpawnFromPool("Enemy1Bullet", firePoint.position, Quaternion.identity);
            tempObject.transform.eulerAngles = new Vector3(firePoint.rotation.x, firePoint.rotation.y, initialRadius - angle - 90);
            tempObject.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            angle += angleStep;
        }
        
        
        
        //
        return new WaitForSeconds(timeBetweenShots);
    }
}
