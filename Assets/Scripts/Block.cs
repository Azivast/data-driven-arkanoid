using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public BlockType type;

    private void OnValidate() {
        if (type is null) Debug.LogError("Block type cannot be null");
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("ball")) {
            BlockHit(); 
        }
    }

    private void BlockHit() { // TODO: Implement damage value from ball ?
        if (type.Destructible) Destroy(this.gameObject);
    }
}
