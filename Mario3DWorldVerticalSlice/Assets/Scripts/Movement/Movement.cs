using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;


public class Movement : MonoBehaviour
{
    [SerializeField] float maxSpeed = 5f; // Max move speed
    [SerializeField] float runSpeedModifier = 2; // Run speed modifier of the original speed
    [SerializeField] float speed = 0f; // Current move speed
    [SerializeField] float acceleration = 0.4f; // Move acceleration
    [SerializeField] float decceleration = 0.75f; // Move decceleration

    [SerializeField] GroundCheck groundCheck; // Check to check if is on ground
    [SerializeField] Transform wallCheckRay; // Transform of the position where the wall check ray will be emitted from

    private bool isRunning = false; // If is running
    [SerializeField] UnityEvent whenRunning; // Invoked when running
    [SerializeField] UnityEvent onDeccel; // Invoked when deccelerating
    [SerializeField] UnityEvent whenWalking; // Invoked when walking
    [SerializeField] UnityEvent onAirtime; // Invoked when in air

    Vector3 velocity = Vector3.zero; // Velocity
    Vector3 inputVector = Vector3.zero; // The input

    public int gamepad = 0; // Used gamepad
    const float wallRayLength = 0.6f; // Length of the ray to do wallchecks

    [SerializeField] Transform playerModel; // Transform of the player model
    Rigidbody rb; // The player rigidbody
    Mover mover; // Object used to move player



    // Setup
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mover = GetComponent<Mover>();
    }



    // Movement
    private void FixedUpdate()
    {

        // Input
        float zInput = Input.GetAxisRaw("Vertical" + gamepad.ToString());
        float xInput = Input.GetAxisRaw("Horizontal" + gamepad.ToString());

        // Velocity is the input vector times the movement speed.
        velocity = inputVector * speed * Time.deltaTime;

        // If the input is not Vector2(0, 0), then the player is allowed to move.
        Vector3 newInput = new Vector3(xInput, 0, zInput);
        if (newInput != Vector3.zero)
        {
            inputVector = newInput;

            // Add acceleration
            if (speed < maxSpeed)
            {
                speed += acceleration;
            }
            else
            {
                if (isRunning == false)
                {
                    speed = Mathf.Clamp(speed, 0, maxSpeed);
                }
                else
                {
                    speed -= 0.1f;
                    if (speed < maxSpeed)
                    {
                        isRunning = false;
                    }
                }
            }

            // Running
            if (groundCheck.isGrounded == true)
            {
                if (Input.GetButton("Run" + gamepad.ToString()))
                {
                    speed *= runSpeedModifier;
                    speed = Mathf.Clamp(speed, 0, maxSpeed * runSpeedModifier);

                    isRunning = true;

                    whenRunning.Invoke();
                }
                else
                {
                    whenWalking.Invoke();
                }
            }
            else
            {
                onAirtime.Invoke();
            }

            // Set the rotation of the player model
            playerModel.rotation = Quaternion.LookRotation(velocity.normalized);
        }
        else
        {
            // Set the current speed to zero when not moving
            if (speed != 0)
            {
                isRunning = false;

                speed -= decceleration;
                speed = Mathf.Clamp(speed, 0, maxSpeed);

                if (groundCheck.isGrounded == true) onDeccel.Invoke();
            }
            else
            {
                speed = 0;
            }
        }


        // Move the player
        if (IsCollidingWithWall() == false)
        {
            mover.Xvelocity = velocity.x;
            mover.Zvelocity = velocity.z;
        }
        else inputVector = Vector3.zero;
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



    // Draw gizmo for debugging
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + inputVector * wallRayLength);
    }
}

