using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponBehaviour : MonoBehaviour
{
    [Tooltip("How often bullets are fired.")] [SerializeField]
    private float FireRate;
    [Tooltip("Prefab of the bullet.")] [SerializeField]
    private GameObject Projectile;
    [Tooltip("Positions at where bullers will be spawned.")] [SerializeField]
    private Transform[] FiringPositions;
    [Tooltip("Sound played when firing.")] [SerializeField]
    private AudioSource firingSound;
    
    private float timer = 0;
    private bool counting = false;
    private PlayerControls controls;
    private InputAction fire;
    
    private void Awake() {
        controls = new PlayerControls();
    }
    
    private void OnEnable() {
        fire = controls.Player.Fire;
        fire.Enable();
    }

    private void OnDisable() {
        fire.Disable();
    }
    
    public void FixedUpdate()
    {
        if (counting) timer -= Time.fixedDeltaTime;
        if (timer <= 0) counting = false;

        if (fire.ReadValue<float>() != 0 && timer <= 0)
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
        firingSound.Play();
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
