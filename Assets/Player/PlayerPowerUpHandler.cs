using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(PlayerBehaviour))]
[RequireComponent(typeof(Collider2D))]
public class PlayerPowerUpHandler : MonoBehaviour {
    public GameObject ActivePowerup;
    private PlayerBehaviour playerBehaviour;

    private void Start() {
        playerBehaviour = GetComponent<PlayerBehaviour>();
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("powerupCapsule")) {
            if (ActivePowerup is not null) {
                Destroy(ActivePowerup);
            }

            ActivePowerup = Instantiate(col.gameObject.GetComponent<PowerupCapsule>().Powerup, transform);
            ActivePowerup.SetActive(true);

            Destroy(col.gameObject);
        }
    }
}