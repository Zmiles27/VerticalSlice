using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Jumping : MonoBehaviour
{
    [SerializeField][Range(1, 1000)] float jumpForce; // The force of the player jump
    [SerializeField][Range(0.1f, 3)] float gravity = 2; // The gravity of the player
    const int jumpFrames = 6; // The total of frames the jump takes

    [SerializeField] GroundCheck groundCheck; // The groundcheck
    [SerializeField] UnityEvent onJump; // Called when jumping

    public int gamepad = 0; // Used gamepad
    bool isJumping = false; // Can jump if this is true
    int frame = 0; // Current frame of the jump

    Rigidbody rb;// The rigidbody

    

    // Setup
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }



    // Input
    private void FixedUpdate()
    {
        // When the jump button is pressed and the player is on the ground, JUMP
        if (Input.GetButton("Jump" + gamepad.ToString()))
        {
            if (groundCheck.isGrounded == true)
            {
                Jump();
                isJumping = true;
            }
        }
        else
        {
            isJumping = false;
            frame = 0;
        }

        // Gravity
        if (rb.useGravity) rb.AddForce(Physics.gravity * (rb.mass * gravity));
    }


    // Jumping
    private void Jump()
    {
        if (isJumping == true)
        {
            ++frame;

            Vector3 jumpVector = new Vector3(0, jumpForce, 0);

            rb.velocity += jumpVector * Time.fixedDeltaTime;

            if (frame >= jumpFrames)
            {
                isJumping = false;
                frame = 0;
            }
        }
    }
}
