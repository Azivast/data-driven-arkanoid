using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerBehaviour : MonoBehaviour {

    [Tooltip("Movement speed (non negative value).")] [SerializeField]
    private float speed = 1;

    [Tooltip("Initial Width of the bar.")]
    public float Size = 1;

    [Tooltip("Prefab of the ball to spawn with.")] [SerializeField]
    private GameObject ball = null;
    
    [Tooltip("Where ball will spawn.")] [SerializeField]
    private Transform ballPosition;
    
    [Tooltip("Direction in which ball is fired at start.")] [SerializeField]
    private Vector2 ballFiringDirection = new Vector2(1, 2);

    [Tooltip("Reference to the Ball when attached to player")] [SerializeField]
    private Rigidbody2D rigidBody = null;

    [Tooltip("Reference to the Collider(required).")] [SerializeField]
    private BoxCollider2D boxCollider;

    [Tooltip("Reference to the Renderer(required).")] [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Vector2 movement;
    private GameObject ballReference;
    
    private PlayerControls controls;
    private InputAction move;
    private InputAction fire;
    

    public void OnValidate() {
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
    }

    private void Start() {
        SetSize(Size);
        SpawnBall();
    }

    private void Awake() {
        controls = new PlayerControls();
    }

    private void OnEnable() {
        move = controls.Player.Move;
        fire = controls.Player.Fire;
        move.Enable();
        fire.Enable();
    }

    private void OnDisable() {
        move.Disable();
        fire.Disable();
    }

    private void FixedUpdate() {
        movement = GetInput();
        movement *= speed * Time.fixedDeltaTime;
        
        // Move attached ball
        if (ballReference is not null) {
            ballReference.transform.position = ballPosition.position;
            if (fire.ReadValue<float>() != 0) ShootBall();
        }
        
        HandleMovement(movement);
    }

    private Vector2 GetInput() {
        Vector2 input = Vector2.zero;
        input.x = move.ReadValue<Vector2>().x;
        return input;
    }

    private void ShootBall() {
        Debug.Log("Shot ball");
        ballReference.GetComponent<Ball>().LaunchBall(ballFiringDirection);
        ballReference = null;
    }

    public void SetSize(float size) {
        spriteRenderer.size = new Vector2(size, spriteRenderer.size.y);
        boxCollider.size = new Vector2(size, boxCollider.size.y);
    }

    private void HandleMovement(Vector2 velocity) {
        rigidBody.velocity = velocity;
    }

    public void OnDeath() {
        SpawnBall();
    }

    private void SpawnBall() {
        Debug.Log("Spawning ball");
        ballReference = Instantiate(ball, transform, false);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(256, 256, 256, 70);
        Gizmos.DrawSphere(ballPosition.position, 0.1f);
        Gizmos.DrawLine(ballPosition.position, ballPosition.position + (Vector3)ballFiringDirection.normalized);
    }
}