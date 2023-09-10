using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    [SerializeField]private GameObject[] sprites;

    private void OnEnable() {
        GameplayManager.Events.OnHealthChange += UpdateHealth;
        
    }

    private void OnDisable() {
        GameplayManager.Events.OnHealthChange -= UpdateHealth;
    }

    private void UpdateHealth(int newHealth) {
        foreach (GameObject sprite in sprites) {
            sprite.SetActive(false);
        }
        for (int i = 0; i < newHealth; i++) {
            sprites[i].SetActive(true);
        }
    }

}
