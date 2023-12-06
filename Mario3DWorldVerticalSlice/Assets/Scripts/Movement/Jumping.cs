using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Jumping : MonoBehaviour
{
    [SerializeField][Range(0, 1)] float jumpForce; // The force of the player jump

    [SerializeField][Range(0.1f, 5)] float gravity = 2; // The gravity of the player

    [SerializeField] GroundCheck groundCheck; // The groundcheck
    [SerializeField] UnityEvent onJump; // Called when jumping

    public int gamepad = 0; // Used gamepad

    Rigidbody rb;// The rigidbody
    Mover mover;// The object to move the player

    

    // Setup
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mover = GetComponent<Mover>();
    }



    // Input
    private void Update()
    {
        // When the jump button is pressed and the player is on the ground, JUMP
        if (Input.GetButtonDown("Jump" + gamepad.ToString()))
        {
            if (groundCheck.isGrounded == true)
            {
                mover.Yvelocity = jumpForce;

                onJump.Invoke();
            }
        }
    }

    // Reset velocity
    private void OnCollisionEnter(Collision collision)
    {
        mover.Yvelocity = 0;
    }



    // Physics
    private void FixedUpdate()
    {
        // Gravity
        if (rb.useGravity) rb.AddForce(Physics.gravity * (rb.mass * gravity));
    }
}
