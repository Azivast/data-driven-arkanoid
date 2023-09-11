using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour {
    [Tooltip("Movement speed (non negative value).")] [SerializeField]
    private float speed = 1f;

    [Tooltip("Reference to the rigid body for the ball (required).")] [SerializeField]
    private Rigidbody2D rigidBody = null;

    [Tooltip("Sound when ball bounces on player.")] [SerializeField]
    private AudioClip bounceSound;
    
    [Tooltip("Minimum y speed ball can have. (Prevents it from getting stuck)")] [SerializeField]
    private float minYSpeed = 0.5f;

    private AudioSource audioSource;
    private bool moving = true;

    public Vector2 Velocity {
        get => rigidBody.velocity;
        set => rigidBody.velocity = value;
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        if (transform.parent is not null) moving = false;
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
        if (moving == false) return;

        rigidBody.velocity = rigidBody.velocity.normalized * speed;

        // The ball can in some edge cases get a strictly horizontal velocity which is unwanted.
        if (rigidBody.velocity.y < minYSpeed && rigidBody.velocity.y > -minYSpeed )
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, -minYSpeed).normalized * speed;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (!other.gameObject.CompareTag("player")) return;
        audioSource.PlayOneShot(bounceSound);
        Vector2 newDirection = transform.position - other.transform.position;
        rigidBody.velocity = newDirection.normalized * speed;
    }

    public void LaunchBall(Vector2 direction) {
        rigidBody.velocity = direction.normalized * speed;
        transform.parent = null;
        moving = true;
    }

    private void OnDisable() {
        GameplayManager.Events.PublishBallAmountChange(-1);
    }
}