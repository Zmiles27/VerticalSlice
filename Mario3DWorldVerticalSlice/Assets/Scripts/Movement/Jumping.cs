using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Jumping : MonoBehaviour
{
    [SerializeField][Range(25, 1000)] float jumpForce; // The force of the player jump
    [SerializeField] GroundCheck groundCheck; // The groundcheck
    [SerializeField] UnityEvent onJump; // Called when jumping

    Rigidbody rb;// The rigidbody


    // Get the rigidbody
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    // Jumping
    private void FixedUpdate()
    {
        // When the jump button is pressed and the player is on the ground, JUMP
        if (Input.GetKey(KeyCode.Space) && groundCheck.isGrounded == true) // BE SURE TO REPLACE WITH UNITYS OWN INPUT LATER
        {
            Vector3 jumpVector = new Vector3(0, jumpForce, 0);
            rb.AddRelativeForce(jumpVector);
        }
    }
}
