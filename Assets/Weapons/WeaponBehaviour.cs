using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public float FireRate;
    public GameObject Projectile;
    public Transform[] FiringPositions;
    
    private float timer = 0;
    private bool counting = false;
    
    public void FixedUpdate()
    {
        if (counting) timer -= Time.fixedDeltaTime;
        if (timer <= 0) counting = false;

        if (Input.GetKey("space") && timer <= 0) // TODO: use input manager?
            Shoot();
    }

    private void Shoot()
    {
        foreach (Transform position in FiringPositions)
        {
            Instantiate(Projectile, position.position, position.rotation);
        }
        counting = true;
        timer = FireRate;
    }

    private void OnDrawGizmos()
    {
        foreach (Transform position in FiringPositions)
        {
            Gizmos.DrawRay(position.position, position.up);
            Gizmos.color = new Color(256, 256, 256, 70);
            Gizmos.DrawSphere(position.position, 0.1f);
        }
    }
}
