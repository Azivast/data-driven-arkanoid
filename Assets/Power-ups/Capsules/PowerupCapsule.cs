using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupCapsule : MonoBehaviour
{
    [Tooltip("Speed at which power up falls.")]
    public float FallSpeed = 2;
    [Tooltip("Color of the capsule.")]
    public Color Color = Color.white;
    [Tooltip("Power up effect which will be activate upon pick-up.")]
    public GameObject Powerup;

    public void OnValidate() {
        if (Powerup is null) {
            Debug.LogError("Power up must not be null.");
        }
        
        Start();
    }
    
    private void Start() {
        GetComponent<SpriteRenderer>().color = Color;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.down * (FallSpeed * Time.fixedDeltaTime));
    }
}
