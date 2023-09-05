using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpLogic : MonoBehaviour
{
    [Tooltip("Duration of power up.")]
    public float EffectTime;
    [Tooltip("Speed at which power up falls.")]
    public float FallSpeed = 1;
    private float timer = 1;
    private bool counting = false;
    private PlayerBehaviour player;
    
    public virtual void PickUp(PlayerBehaviour player)
    {
        StartTimer();
        GetComponent<BoxCollider2D>().enabled = false; //TODO: implement differently?
        GetComponent<SpriteRenderer>().enabled = false;
    }

    protected virtual void Remove(PlayerBehaviour player)
    {
        Destroy(this.gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            player = other.gameObject.GetComponent<PlayerBehaviour>();
            PickUp(player);
        }
    }

    private void StartTimer()
    {
        timer = EffectTime;
        counting = true;
    }

    private void Update()
    {
        if (!counting) return;
        timer -= Time.deltaTime;
        if (timer < 0)  Remove(player);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.down * (FallSpeed * Time.fixedDeltaTime));
    }
    
    protected virtual void OnValidate() {
        if (EffectTime <= 0) {
            Debug.LogWarning("Effect Time cannot be 0 or below.");
            EffectTime = 1;
        }
    }
}
