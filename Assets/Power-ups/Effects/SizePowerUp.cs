using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SizePowerUp : MonoBehaviour {
    [Tooltip("Width the player will get during the power up.")]
    public float NewSize;

    private PlayerBehaviour player;

    void OnValidate() {
        if (NewSize < 1) {
            Debug.LogWarning("New size cannot be below 1.");
            NewSize = 1;
        }
    }

    void OnEnable() {
        player = GetComponentInParent<PlayerBehaviour>();
        player.SetSize(NewSize);
    }

    void OnDisable() {
        player.SetSize(player.Size);
    }
}