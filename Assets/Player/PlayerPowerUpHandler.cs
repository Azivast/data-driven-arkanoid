using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerBehaviour))]
[RequireComponent(typeof(Collider2D))]
public class PlayerPowerUpHandler : MonoBehaviour
{
    public PowerUp ActivePowerup;
    private PlayerBehaviour playerBehaviour;

    private void Start() {
        playerBehaviour = GetComponent<PlayerBehaviour>();
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("powerup")) {
            if (ActivePowerup is not null) ActivePowerup.Remove(playerBehaviour);
            ActivePowerup = col.gameObject.GetComponent<PowerUp>(); //TODO: Optimize?
            ActivePowerup.PickUp(playerBehaviour);
        }
    }
}
