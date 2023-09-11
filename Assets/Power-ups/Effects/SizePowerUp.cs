using UnityEngine;

public class SizePowerUp : MonoBehaviour {
    [Tooltip("Width the player will get during the power up.")]
    public float NewSize;

    private PlayerBehaviour player;

    private void OnValidate() {
        if (NewSize < 1) {
            Debug.LogWarning("New size cannot be below 1.");
            NewSize = 1;
        }
    }
    
    private void OnEnable() {
        player = GetComponentInParent<PlayerBehaviour>();
        player.SetSize(NewSize);
    }

    private void OnDisable() {
        player.SetSize(player.Size);
    }
}