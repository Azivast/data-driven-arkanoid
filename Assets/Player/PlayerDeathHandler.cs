using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(PlayerBehaviour))]
[RequireComponent(typeof(AudioSource))]
public class PlayerDeathHandler : MonoBehaviour
{
    [Tooltip("Sound played when player dies.")] [SerializeField]
    private AudioClip DeathSound;
    [Tooltip("Duration of camera shake when dying.")] [SerializeField]
    private float cameraShakeDuration = 1;
    [Tooltip("Intensity of camera shake when dying.")] [SerializeField]
    private float cameraShakeIntensity = 1;
    private AudioSource audioSource;
    private PlayerBehaviour behaviour;

    private int health => PlayerStats.Health;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
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
        CameraShake.Shake(cameraShakeDuration, cameraShakeIntensity);
        
        GameplayManager.Events.PublishHealthChange(-1);
        
        if (health <= 0) GameplayManager.Events.PublishGameOver();
        else behaviour.OnDeath();
    }
}
