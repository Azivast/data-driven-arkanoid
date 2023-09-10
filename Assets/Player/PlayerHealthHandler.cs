using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(PlayerBehaviour))]
public class PlayerHealthHandler : MonoBehaviour
{
    [Tooltip("The number of balls the player can lose before the game is over. (Minimum: 1)")] [SerializeField]
    private int health = 3;

    private PlayerBehaviour behaviour;

    private void Start() {
        GameplayManager.Events.PublishHealthChange(health);
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
        Debug.Log("Died");
        health--;
        GameplayManager.Events.PublishHealthChange(health);
        behaviour.OnDeath();
        if (health <= 0) GameplayManager.Events.PublishGameOver();
    }
}
