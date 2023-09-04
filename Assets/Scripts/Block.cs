using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]private Color color = Color.blue;
    [SerializeField]private int health = 1;
    [SerializeField]private int pointValue = 1;
    [SerializeField]private bool destructible = true;
    [SerializeField]private GameObject powerUp = null;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "ball") {
            BlockHit(); 
        }
    }

    private void BlockHit() { // TODO: Implement damage value from ball ?
        if (destructible) Destroy(this.gameObject);
    }
}
