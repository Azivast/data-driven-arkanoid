using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteAlways]
[RequireComponent(typeof(SpriteRenderer))]
public class Block : MonoBehaviour {
    public BlockType type;
    [Tooltip("Duration of camera shake upon destroy.")] [SerializeField]
    private float camShakeDuration = 0.05f;
    [Tooltip("Intensity of camera shake upon destroy.")] [SerializeField]
    private float camShakeIntensity = 0.1f;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private int hp;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponentInParent<AudioSource>();
        SetSprite();
        hp = type.Health;
    }

    private void OnValidate() {
        if (type is null) {
            Debug.LogError("Block type cannot be null.");
            return;
        }
        SetSprite();
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("ball")) {
            BlockHit();
        }
    }

    private void SetSprite() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = type.Sprite;
        spriteRenderer.color = type.Color;
    }

    public void BlockHit() {
        audioSource.PlayOneShot(type.HitSound);
        if (!type.Destructible) return;

        hp--;
        if (hp > 0) return;

        // Power-up drops
        if (type.PowerUpSet is not null) { // not all block types must have power-ups
            System.Random rand = new System.Random();
            if (type.ChanceOfPowerUp != 0 && rand.Next(1, 100) <= type.ChanceOfPowerUp) {
                GameObject powerUp =
                    type.PowerUpSet.PowerupCapsules[rand.Next(0, type.PowerUpSet.PowerupCapsules.Count())];
                Instantiate(powerUp, transform.position, transform.rotation);
            }
        }
        
        GameplayManager.Events.PublishScoreChange(+type.Value);
        GameplayManager.Events.PublishBlockDestroyed();
        CameraShake.Shake(camShakeDuration, camShakeIntensity);
        Destroy(this.gameObject);
    }
}