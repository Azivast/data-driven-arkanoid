using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Block : MonoBehaviour
{
    public BlockType type;
    public GameObject PowerUp = null;
    public SpriteRenderer spriteRenderer;

    private void OnValidate() {
        if (type is null) {
            Debug.LogError("Block type cannot be null");
        }
        if (spriteRenderer is null) {
            Debug.LogError("Sprite renderer cannot be null");
        }

        spriteRenderer.sprite = type.Sprite;
        spriteRenderer.color = type.Color;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("ball")) {
            BlockHit(); 
        }
    }

    private void BlockHit() { // TODO: Implement damage value from ball ?
        if (!type.Destructible) return;
        if (PowerUp is not null) Instantiate(PowerUp, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
