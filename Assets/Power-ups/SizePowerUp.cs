using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SizePowerUp : PowerUpLogic
{
    [Tooltip("Width the player will get during the power up.")]
    public float NewSize;
    
    protected override void OnValidate()
    {
        base.OnValidate();
        if (NewSize < 1) {
            Debug.LogWarning("New size cannot be below 1.");
            NewSize = 1;
        }
    }

    public override void PickUp(PlayerBehaviour player) {
        base.PickUp(player);
        player.SetSize(NewSize);
    }

    protected override void Remove(PlayerBehaviour player) {
        base.Remove(player);
        player.SetSize(player.Size);
    } 
}
