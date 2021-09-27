using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YAZ_ShipMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float speed = 1;

    private Vector2 movement;
    
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(movement * speed * Time.fixedDeltaTime);
    }
}
