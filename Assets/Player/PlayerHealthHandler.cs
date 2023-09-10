using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(PlayerBehaviour))]
[RequireComponent(typeof(AudioSource))]
public class PlayerHealthHandler : MonoBehaviour
{
    [Tooltip("The number of balls the player can lose before the game is over. (Minimum: 1)")] [SerializeField]
    private int health = 3;
    [Tooltip("Sound played when player dies.")] [SerializeField]
    private AudioClip DeathSound;
    [Tooltip("Duration of camera shake when dying.")] [SerializeField]
    private float cameraShakeDuration = 1;
    [Tooltip("Intensity of camera shake when dying.")] [SerializeField]
    private float cameraShakeIntensity = 1;
    private AudioSource audioSource;
    private PlayerBehaviour behaviour;


    private void Start() {
        GameplayManager.Events.PublishHealthChange(health);
        audioSource = GetComponent<AudioSource>();
    }

    public void OnValidate() {
        if (health < 1) {
            health = 1;
            Debug.LogWarning("Health can not be 0 or a negative number.");
        }
    }

    private void OnEnable() {
        GameplayManager.Events.OnPlayerDeath += OnDeath;
        behaviour = GetComponent<PlayerBehaviour>();
    }

    private void OnDisable() {
        GameplayManager.Events.OnPlayerDeath -= OnDeath;
    }

    private void OnDeath() {
        audioSource.PlayOneShot(DeathSound);
        health--;
        GameplayManager.Events.PublishHealthChange(health);
        behaviour.OnDeath();
        CameraShake.Shake(cameraShakeDuration, cameraShakeIntensity);
        if (health <= 0) GameplayManager.Events.PublishGameOver();
    }
}
