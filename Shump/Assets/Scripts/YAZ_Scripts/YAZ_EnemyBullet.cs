using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YAZ_EnemyBullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public Vector2 trajectory = Vector2.left;
    
    void Start()
    {
        rb.velocity =  trajectory * speed;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player"))
        {
            YAZ_GameManager.instance.LifeLost();
        }
        
        if (!hitInfo.CompareTag("Bullet"))
        {
            if (!hitInfo.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
        }
    }
}
