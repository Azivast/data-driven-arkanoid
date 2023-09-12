using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ProjectileBehaviour : MonoBehaviour {
    public int Speed;

    private void FixedUpdate() {
        transform.position += transform.up * (Speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.collider.CompareTag("block")) {
            col.gameObject.GetComponent<Block>().BlockHit();
        }

        Destroy(this.gameObject);
    }
}