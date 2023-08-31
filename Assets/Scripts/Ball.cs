using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private BallData data;
    private Rigidbody2D rb;
    private Vector2 velocity;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        rb.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        
    }
}

public class BallData
{
    private Vector2 Speed;
}

