using UnityEngine;

public class PowerupCapsule : MonoBehaviour {
    [Tooltip("Speed at which power up falls.")] [SerializeField]
    private float FallSpeed = 2;

    [Tooltip("Color of the capsule.")] [SerializeField]
    private Color Color = Color.white;

    [Tooltip("Power up effect which will be activate upon pick-up.")]
    public GameObject Effect;

    public void OnValidate() {
        if (Effect is null) 
            Debug.LogError("Power up must not be null.");

        Start();
    }
    
    private void Start() {
        GetComponent<SpriteRenderer>().color = Color;
    }

    private void FixedUpdate() {
        transform.Translate(Vector3.down * (FallSpeed * Time.fixedDeltaTime));
    }
}