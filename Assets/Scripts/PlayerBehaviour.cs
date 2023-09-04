using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBehaviour : MonoBehaviour
{
    [Tooltip("The number of balls the player can lose before the game is over. (Minimum: 1)")]
    [SerializeField]private int health = 3;
    [Tooltip("Movement speed (non negative value).")]
    [SerializeField]private float speed = 1;
    [Tooltip("Width of the bar.")]
    [SerializeField]private float size = 1;
    [Tooltip("Reference to the rigid body for the player (required).")]
    [SerializeField]private Rigidbody2D rigidBody = null;

    private Vector2 movement;

    public void OnValidate() {
        if (health < 1) {
            health = 1;
            Debug.LogWarning("Health can not be 0 or a negative number.");
        }
        if (speed < 0) {
            speed = 0;
            Debug.LogWarning("Speed must be a non negative number");
        }
        if (rigidBody is null) {
            speed = 0;
            Debug.LogWarning("Rigidbody cannot be null");
        }
    }

    private void FixedUpdate()
    {
        movement = GetInput();
        //TODO: Verify movement and how deltaTime is used, movement is not smooth
        HandleMovement(movement * speed * Time.fixedDeltaTime);
    }

    private Vector2 GetInput()
    {
        Vector2 input = Vector2.zero;
        if (Input.GetKey("a")) // TODO: use input manager?
        {
            input += Vector2.left;
        }
        if (Input.GetKey("d"))
        {
            input += Vector2.right;
        }
        return input;
    }

    private void HandleMovement(Vector2 velocity)
    {
        rigidBody.velocity = velocity;
    }
    
    
}
