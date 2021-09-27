using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YAZ_EnemyMovement : MonoBehaviour
{
    private Rigidbody2D enemyRb;
    private Transform playerTf;
    public float speed = 2;

    void Awake()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //L'ennemi est au dessus du joueur
        if (transform.position.y >= YAZ_GameManager.instance.playerShip.transform.position.y)
        {
            enemyRb.MovePosition((Vector2)transform.position + Vector2.up * speed * Time.fixedDeltaTime);
        }
        else
        {
            enemyRb.MovePosition((Vector2)transform.position + Vector2.down * speed * Time.fixedDeltaTime);
        }
    }
}
