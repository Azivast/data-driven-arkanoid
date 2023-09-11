using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPowerUp : MonoBehaviour {
    [Tooltip("The weapon the player will get during the power up.")]
    public GameObject weapon;

    private GameObject weaponInstance;
    private PlayerBehaviour player;

    private void OnValidate() {
        if (weapon is null) {
            Debug.LogError("Weapon cannot be null.");
        }
    }

    private void OnEnable() {
        player = GetComponentInParent<PlayerBehaviour>();
        weaponInstance = Instantiate(weapon, player.transform, false);
    }

    private void OnDisable() {
        Destroy(weaponInstance);
    }
}