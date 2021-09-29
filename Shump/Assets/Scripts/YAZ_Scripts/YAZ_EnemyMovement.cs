using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YAZ_EnemyMovement : MonoBehaviour
{
    private Rigidbody2D enemyRb;
    private Transform playerTf;
    private YAZ_EnemyShooting enemyShooting;
    public float speed = 2;
    
    //Variable pour bouger le vaisseau que si le joueur est trop loin verticalement
    public float maxProximity = 0.05f;

    //Variables pour rediriger l'ennemi s'il va en dehors de l'écran de jeu
    public float upperLimit = 7;
    public float upperReposition = 4;
    public float lowerLimit = -7;
    public float lowerReposition = -4;
    private bool hasReachedLimit = false;
    

    void Awake()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        enemyShooting = GetComponent<YAZ_EnemyShooting>();
    }

    private void FixedUpdate()
    {
        if (!hasReachedLimit)
        {
            MoveEnemy();
        }
        else
        {
            Recenter();
        }
    }

    

    private void MoveEnemy()
    {
        //L'ennemi est entre les deux limites
        if (transform.position.y > enemyShooting.playerShipPos.transform.position.y + maxProximity)
        {
            //Debug.Log("Je me déplace (vers bas)");
            //L'ennemi est au dessus du joueur : il descend
            enemyRb.MovePosition((Vector2) transform.position + Vector2.down * speed * Time.fixedDeltaTime);
        }
        else if (transform.position.y < enemyShooting.playerShipPos.transform.position.y - maxProximity)
        {
            //Debug.Log("Je me déplace (vers haut)");
            //L'ennemi est en dessous du joueur : il remonte
            enemyRb.MovePosition((Vector2) transform.position + Vector2.up * speed * Time.fixedDeltaTime);
        }
        else
        {
            //Debug.Log("coucou j'attaque");
            enemyShooting.EnemyShoot();
        }
        
        if (transform.position.y > upperLimit)
        {
            //Debug.Log("trop haut !");
            //L'ennemi est trop haut : on le redescend
            hasReachedLimit = true;
        }
        else if (transform.position.y < lowerLimit)
        {
            //Debug.Log("trop bas !");
            //L'ennemi est trop bas : on le remonte
            hasReachedLimit = true;
        }


        
    }
    private void Recenter()
         {
             if (transform.position.y >= upperReposition)
             {
                 enemyRb.MovePosition((Vector2) transform.position + Vector2.down * speed * Time.fixedDeltaTime);
                 //Debug.Log("Redescends !");
             }
             else if (transform.position.y <= lowerReposition)
             {
                 enemyRb.MovePosition((Vector2) transform.position + Vector2.up * speed * Time.fixedDeltaTime);
                 //Debug.Log("Remonte !");
             }
             else
             {
                 Debug.Log("C'est bon !");
                 hasReachedLimit = false;
             }
         }
}
