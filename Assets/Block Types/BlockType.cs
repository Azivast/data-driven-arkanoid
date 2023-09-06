using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Block Type", menuName = "Arkanoid/BlockType", order = 1)]
public class BlockType : ScriptableObject
{
    public Sprite Sprite;
    public Color Color = Color.white;
    [Tooltip("Score the tile gives.")]
    public int Value = 1;
    public int Health = 1;
    public bool Destructible = true;
    [Tooltip("Percentage that a power-up is dropped upon destruction.")]
    [Range(0, 100)]
    public int ChanceOfPowerUp = 1;
    [Tooltip("The set of power-ups that could drop.")]
    public PowerUpSet PowerUpSet;

    private void OnValidate() {
        if (Sprite is null) Debug.LogError("Sprite cannot be null");

        if (Health < 1) {
            Health = 1;
            Debug.LogWarning("Health must not be 0 or a negative number.");
        }
        if (Value < 1) {
            Value = 1;
            Debug.LogWarning("Point must not a negative number.");
        }
    }
}
