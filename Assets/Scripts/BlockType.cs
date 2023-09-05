using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Block Type", menuName = "Arkanoid/BlockType", order = 1)]
public class BlockType : ScriptableObject
{
    public Sprite Sprite;
    public Color Color = Color.white;
    public int Health = 1;
    public int PointValue = 1;
    public bool Destructible = true;
    public GameObject PowerUp = null;
    
    private void OnValidate() {
        if (Sprite is null) Debug.LogError("Sprite cannot be null");
        
        if (Health < 1) {
            Health = 1;
            Debug.LogWarning("Health must not be 0 or a negative number.");
        }
        if (PointValue < 1) {
            PointValue = 1;
            Debug.LogWarning("Point must not a negative number.");
        }
    }
}
