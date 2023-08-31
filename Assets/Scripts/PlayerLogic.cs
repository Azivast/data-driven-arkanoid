using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(PlayerData))]
public class PlayerLogic : MonoBehaviour
{
    private PlayerData data;
    private new Rigidbody2D rigidbody;
    Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        data = GetComponent<PlayerData>(); // Todo: error handling?
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = GetInput(); // TODO: change order of multi
    }

    private void FixedUpdate()
    {
        
        //TODO: Verify movement and how deltaTime is used, movement is not smooth
        HandleMovement(movement * data.speed * Time.fixedDeltaTime);
    }

    private Vector2 GetInput()
    {
        Vector2 input = Vector2.zero;
        if (Input.GetKey("a")) // TODO: use input manager?
        {
            input += Vector2.left;
        }
        if (Input.GetKey("d"))
        {
            input += Vector2.right;
        }
        return input;
    }

    private void HandleMovement(Vector2 velocity)
    {
        rigidbody.velocity = velocity;
    }
    
    
}
