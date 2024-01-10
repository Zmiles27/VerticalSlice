using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f; // Move speed
    Vector3 velocity = Vector3.zero; // Velocity

    Rigidbody rb; // The rigidbody


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    // Movement
    private void FixedUpdate()
    {
        // Get the input
        float zInput = Input.GetAxisRaw("Vertical");
        float xInput = Input.GetAxisRaw("Horizontal");

        // Velocity is the input vector times the movement speed.
        velocity = new Vector3(xInput, 0, zInput) * speed * Time.deltaTime;

        // If the velocity is not Vector2(0, 0), then the player is allowed to move.
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(transform.position + velocity);
        }
    }
}
