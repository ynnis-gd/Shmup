using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YAZ_GameManager : MonoBehaviour
{
    public static YAZ_GameManager instance;

    public GameObject playerShip;
    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
