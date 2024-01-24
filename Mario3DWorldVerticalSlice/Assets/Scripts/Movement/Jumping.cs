using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Jumping : MonoBehaviour
{
    [SerializeField][Range(0.23f, 1)] float jumpForce = 0.23f; // The force of the player jump

    [SerializeField][Range(0.1f, 5)] float gravity = 2; // The gravity of the player
    [SerializeField][Range(0.1f, 5)] float minGravity = 1.2f; // The minimum gravity of the player
    const float gravityDepletion = 0.1f;
    float currentGravity;

    [SerializeField] GroundCheck groundCheck; // The groundcheck
    [SerializeField] UnityEvent onJump; // Called when jumping

    public int gamepad = 0; // Used gamepad

    Rigidbody rb;// The rigidbody
    Mover mover;// The object to move the player
    Dive dive; // Diving script



    // Setup
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mover = GetComponent<Mover>();
        dive = GetComponent<Dive>();

        currentGravity = gravity;
    }



    // Input
    private void Update()
    {
        // When the jump button is pressed and the player is on the ground, JUMP
        if (Input.GetButtonDown("Jump" + gamepad.ToString()) && groundCheck.isGrounded == true)
        {
            mover.Yvelocity = jumpForce;

            onJump.Invoke();
        }
        if (Input.GetButton("Jump" + gamepad.ToString()) && groundCheck.isGrounded == false)
        {
            if (currentGravity > minGravity)
            {
                currentGravity -= gravityDepletion;
            }
        }

        if (groundCheck.isGrounded == true)
        {
            currentGravity = gravity;
        }
    }

    // Reset velocity
    private void OnCollisionEnter(Collision collision)
    {
        ResetJumpVelocity();
    }

    public void ResetJumpVelocity()
    {
        if (mover != null) { mover.Yvelocity = 0; }
    }



    // Physics
    private void FixedUpdate()
    {
        // Gravity
        if (rb.useGravity && groundCheck.isGrounded == false && dive.isDiving == false) rb.AddForce(Physics.gravity * (rb.mass * currentGravity));
    }
}
