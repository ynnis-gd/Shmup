using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YAZ_GameManager : MonoBehaviour
{
    public static YAZ_GameManager instance;

    public GameObject playerShip;
    public int playerLives = 3;
    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    public void LifeLost()
    {
        playerLives--;
        if (playerLives <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}
