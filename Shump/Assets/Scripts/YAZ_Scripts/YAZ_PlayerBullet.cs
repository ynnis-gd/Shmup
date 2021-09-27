using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YAZ_PlayerBullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public Rigidbody2D rb;
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.CompareTag("Enemy"))
        {
            //Debug.Log("y'a un ennemi !");
            YAZ_EnemyStats enemy = hitInfo.GetComponent<YAZ_EnemyStats>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
        

        if (!hitInfo.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
