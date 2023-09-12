using System.Collections;
using System.Linq;
using UnityEngine;
using Random = System.Random;

[ExecuteAlways]
[RequireComponent(typeof(SpriteRenderer))]
public class Block : MonoBehaviour {
    [Tooltip("Type of block.")] 
    public BlockType type;

    [Tooltip("Duration of camera shake upon destroy.")] [SerializeField]
    private float camShakeDuration = 0.05f;

    [Tooltip("Intensity of camera shake upon destroy.")] [SerializeField]
    private float camShakeIntensity = 0.1f;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private int hp;

    private void OnValidate() {
        if (type is null) {
            Debug.LogError("Block type cannot be null.");
            return;
        }

        SetSprite();
    }

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponentInParent<AudioSource>();
        SetSprite();
        hp = type.Health;
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("ball")) BlockHit();
    }

    private void SetSprite() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = type.Sprite;
        spriteRenderer.color = type.Color;
    }

    public void BlockHit() {
        audioSource.PlayOneShot(type.HitSound);
        CameraShake.Shake(camShakeDuration, camShakeIntensity);
        if (!type.Destructible) return;

        hp--;
        if (hp > 0) return;

        // Power-up drops
        if (type.PowerUpSet is not null) { // not all block types must have power-ups
            var rand = new Random();
            if (type.ChanceOfPowerUp != 0 && rand.Next(1, 100) <= type.ChanceOfPowerUp) {
                var powerUp =
                    type.PowerUpSet.PowerupCapsules[rand.Next(0, type.PowerUpSet.PowerupCapsules.Count())];
                Instantiate(powerUp, transform.position, transform.rotation, GameObject.FindWithTag("level").transform);
            }
        }

        GameplayManager.Events.PublishScoreChange(+type.Value);
        Destroy(gameObject);
    }
    
    private void OnDestroy() {
        if (type.Destructible) GameplayManager.Events.PublishBlockDestroyed();
    }
}