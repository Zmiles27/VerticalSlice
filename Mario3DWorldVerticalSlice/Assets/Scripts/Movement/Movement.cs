using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    [SerializeField] float maxSpeed = 5f; // Max move speed
    [SerializeField] float runSpeedModifier = 2; // Run speed modifier of the original speed
    [SerializeField] float speed = 0f; // Current move speed
    [SerializeField] float acceleration = 0.4f; // Move acceleration

    [SerializeField] Transform wallCheckRay; // Transform of the position where the wall check ray will be emitted from

    Vector3 velocity = Vector3.zero; // Velocity
    Vector3 inputVector = Vector3.zero; // The input

    const float wallRayLength = 0.6f;

    Rigidbody rb;


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
        inputVector = new Vector3(xInput, 0, zInput);

        // Velocity is the input vector times the movement speed.
        velocity = inputVector * speed * Time.deltaTime;

        // If the input is not Vector2(0, 0), then the player is allowed to move.
        if (inputVector != Vector3.zero)
        {
            // Add acceleration
            if (speed < maxSpeed)
            {
                speed += acceleration;
            }
            else
            {
                speed = Mathf.Clamp(speed, 0, maxSpeed);
            }
        }
        else
        {
            // Set the current speed to zero when not moving
            speed = 0;
        }

        // Running
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) // REPLACE WITH UNITY INPUT LATER
        {
            speed *= runSpeedModifier;
        }


        // Move the player
        if (IsCollidingWithWall() == false)
        {
            rb.MovePosition(transform.position + velocity);
        }
    }


    // Check with a raycast if the player is colliding with a wall
    bool IsCollidingWithWall()
    {
        if (Physics.Raycast(wallCheckRay.position, inputVector, wallRayLength))
        {
            return true;
        }
        return false;

    }
}
