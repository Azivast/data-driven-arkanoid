using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [Tooltip("Speed at which power up falls.")]
    public float FallSpeed = 2;
    private PlayerBehaviour player;
    
    protected virtual void OnValidate() { }
    
    public virtual void PickUp(PlayerBehaviour player)
    {
        GetComponent<BoxCollider2D>().enabled = false; //TODO: implement differently?
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public virtual void Remove(PlayerBehaviour player)
    {
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.down * (FallSpeed * Time.fixedDeltaTime));
    }
}
