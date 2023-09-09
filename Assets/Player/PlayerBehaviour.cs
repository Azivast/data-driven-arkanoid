using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerBehaviour : MonoBehaviour {
    [Tooltip("The number of balls the player can lose before the game is over. (Minimum: 1)")] [SerializeField]
    private int health = 3;

    [Tooltip("Movement speed (non negative value).")] [SerializeField]
    private float speed = 1;

    [Tooltip("Initial Width of the bar.")]
    public float Size = 1;

    [Tooltip("Reference to the rigid body(required).")] [SerializeField]
    private GameObject ball = null;
    
    [Tooltip("Direction in which ball is fired at start.")] [SerializeField]
    private Vector2 ballFiringDirection = new Vector2(1, 2);

    [Tooltip("Reference to the Ball when attached to player")] [SerializeField]
    private Rigidbody2D rigidBody = null;

    [Tooltip("Reference to the Collider(required).")] [SerializeField]
    private BoxCollider2D boxCollider;

    [Tooltip("Reference to the Renderer(required).")] [SerializeField]
    private SpriteRenderer spriteRenderer;

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

        if (Size < 0) {
            Size = 1;
            Debug.LogWarning("Size must be a non negative number.");
        }

        if (rigidBody is null) {
            Debug.LogWarning("Rigidbody cannot be null");
        }
        
        Start();
    }

    private void Start() {
        SetSize(Size);
    }

    private void FixedUpdate() {
        movement = GetInput();
        movement *= speed * Time.fixedDeltaTime;
        // Shoot Ball
        if (ball is not null) {
            //ball.GetComponent<Ball>().Velocity = Vector2.zero;  // TODO: Optimize
            ball.transform.position = new Vector3(transform.position.x, ball.transform.position.y);
            if (Input.GetKey("space")) ShootBall();
        }


        //TODO: Verify movement and how deltaTime is used, movement is not smooth
        HandleMovement(movement);
    }

    private Vector2 GetInput() {
        Vector2 input = Vector2.zero;
        if (Input.GetKey("a")) // TODO: use input manager?
        {
            input += Vector2.left;
        }

        if (Input.GetKey("d")) {
            input += Vector2.right;
        }

        return input;
    }

    private void ShootBall() {
        ball.GetComponent<Ball>().Velocity = ballFiringDirection.normalized * Time.fixedDeltaTime; // TODO: Optimize
        ball.transform.parent = null;
        ball = null;
    }

    public void SetSize(float size) {
        spriteRenderer.size = new Vector2(size, spriteRenderer.size.y);
        boxCollider.size = new Vector2(size, boxCollider.size.y);
    }

    private void HandleMovement(Vector2 velocity) {
        rigidBody.velocity = velocity;
    }
}