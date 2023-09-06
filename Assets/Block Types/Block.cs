using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteAlways]
[RequireComponent(typeof(SpriteRenderer))]
public class Block : MonoBehaviour
{
    public BlockType type;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        SetSprite();
    }

    private void OnValidate() {
        if (type is null) {
            Debug.LogError("Block type cannot be null");
            return;
        }

        SetSprite();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("ball")) {
            BlockHit(); 
        }
    }

    private void SetSprite()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = type.Sprite;
        spriteRenderer.color = type.Color;
    }

    private void BlockHit() { // TODO: Implement damage value from ball ?
        if (!type.Destructible) return;
        
        // Power-up drops
        if (type.PowerUpSet is not null)
        {
            System.Random rand = new System.Random();
            if (type.ChanceOfPowerUp != 0 && rand.Next(1, 100) <= type.ChanceOfPowerUp) {
                GameObject powerUp = type.PowerUpSet.PowerUps[rand.Next(0, type.PowerUpSet.PowerUps.Count())];
                Instantiate(powerUp, transform.position, transform.rotation);
            }
        }
        Destroy(this.gameObject);
    }
}
