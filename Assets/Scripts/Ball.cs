using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Tooltip("Movement speed (non negative value).")]
    [SerializeField]private float speed = 1f;
    [Tooltip("Reference to the rigid body for the ball (required).")]
    [SerializeField]private Rigidbody2D rigidBody = null;

    private Vector2 velocity;
    void Start() {
        velocity = Vector2.down * speed;
    }
    void FixedUpdate() {
        rigidBody.velocity = velocity;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 newDirection;
        
        // Player collision
        if (collision.gameObject.TryGetComponent<PlayerBehaviour>(out _))
        {
            newDirection = transform.position - collision.transform.position;
            velocity = newDirection.normalized * speed;
            return;
        }
        
        // Other collisions
        Vector2 collisionNormal = collision.contacts[0].normal; // Todo check against more than just [0] ?
        Debug.Log(collision.contactCount);
        newDirection = Vector2.Reflect(velocity, collisionNormal);
        velocity = newDirection.normalized* speed;
    }
}

