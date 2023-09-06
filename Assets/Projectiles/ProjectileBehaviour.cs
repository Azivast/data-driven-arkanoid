using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ProjectileBehaviour : MonoBehaviour
{
    public int Speed;

    private void FixedUpdate()
    {
        transform.position += transform.up * (Speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("block"))
        {
            col.gameObject.GetComponent<Block>().BlockHit();
            Destroy(this.gameObject);
        }
    }
}
