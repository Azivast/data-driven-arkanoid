using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPowerUp : PowerUp
{
    [Tooltip("The weapon the player will get during the power up.")]
    public GameObject weapon;

    private GameObject weaponInstance;
    
    protected override void OnValidate()
    {
        base.OnValidate();
        if (weapon is null) {
            Debug.LogWarning("Weapon cannot be null.");
        }
    }

    public override void PickUp(PlayerBehaviour player) {
        base.PickUp(player);
        weaponInstance = Instantiate(weapon, player.transform, false);
    }

    public override void Remove(PlayerBehaviour player) {
        Destroy(weaponInstance);
        base.Remove(player);
    } 
}
