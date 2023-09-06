using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SizePowerUp : PowerUp
{
    [Tooltip("Width the player will get during the power up.")]
    public float NewSize;
    
    protected void OnValidate()
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

    public override void Remove(PlayerBehaviour player) {
        player.SetSize(player.Size);
        base.Remove(player);
    } 
}
