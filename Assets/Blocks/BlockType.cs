using UnityEngine;

[CreateAssetMenu(fileName = "Block Type", menuName = "Arkanoid/BlockType")]
public class BlockType : ScriptableObject {
    [Tooltip("Sprite of the tile.")] 
    public Sprite Sprite;
    
    [Tooltip("Color of the sprite.")] 
    public Color Color = Color.white;

    [Tooltip("Score the tile gives when destroyed.")] 
    public int Value = 1;

    [Tooltip("Number of hits before block breaks.")] 
    public int Health = 1;
    
    [Tooltip("Whether block can be destroyed by player.")] 
    public bool Destructible = true;

    [Tooltip("Percentage that a power-up is dropped upon destruction.")] [Range(0, 100)]
    public int ChanceOfPowerUp = 1;

    [Tooltip("The set of power-ups that could drop.")]
    public PowerUpSet PowerUpSet;

    [Tooltip("Sound played when hit by other object.")] 
    public AudioClip HitSound;

    private void OnValidate() {
        if (Sprite is null) 
            Debug.LogError("Sprite cannot be null");

        if (Value < 1) {
            Value = 1;
            Debug.LogWarning("Point must not a negative number.");
        }
        
        if (Health < 1) {
            Health = 1;
            Debug.LogWarning("Health must not be 0 or a negative number.");
        }

        if (HitSound is null) 
            Debug.LogError("Hit sound missing.");
    }
}