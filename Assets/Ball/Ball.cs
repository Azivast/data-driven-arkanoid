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

    private Vector2 velocity = Vector2.zero;

    public Vector2 Velocity {
        get => velocity;
        set => velocity = value.normalized * speed;
    }
    
    private void OnValidate() {
        if (rigidBody is null) Debug.LogError("Sprite cannot be null");
        if (speed < 0) {
            speed = 0;
            Debug.LogWarning("Speed must be a non negative number");
        }
    }

    private void OnEnable() {
        GameplayManager.Events.PublishBallAmountChange(+1);
    }

    void FixedUpdate() {
        rigidBody.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 newDirection;
        
        // Player collision
        if (other.gameObject.CompareTag("player"))
        {
            newDirection = transform.position - other.transform.position;
            newDirection += Vector2.up; // Make ball always move a little more up than horizontal
            velocity = newDirection.normalized * speed;
            return;
        }
        
        // Other collisions TODO: This is a bit broken?
        Vector2 collisionNormal = new Vector2();
        foreach (ContactPoint2D contact in other.contacts) {
            collisionNormal += contact.normal;
        }
        collisionNormal.Normalize();
        
        newDirection = Vector2.Reflect(velocity, collisionNormal);
        velocity = newDirection.normalized* speed;
    }
    
    private void OnDisable() {
        GameplayManager.Events.PublishBallAmountChange(-1);
    }
}

