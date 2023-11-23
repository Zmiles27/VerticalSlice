using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Jumping : MonoBehaviour
{
    [SerializeField][Range(25, 1000)] float jumpForce; // The force of the player jump
    [SerializeField] GroundCheck groundCheck; // The groundcheck
    [SerializeField] UnityEvent onJump; // Called when jumping
    bool isJumping = false;
    int count = 0;
    Rigidbody rb;// The rigidbody


    // Get the rigidbody
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    // Input
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && groundCheck.isGrounded == true)
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
            count = 0;
        }
    }


    // Jumping
    private void FixedUpdate()
    {
        // When the jump button is pressed and the player is on the ground, JUMP
        if (isJumping == true) // BE SURE TO REPLACE WITH UNITYS OWN INPUT LATER
        {
            ++count;
            print(count);
            Vector3 jumpVector = new Vector3(0, jumpForce, 0);
            rb.AddRelativeForce(jumpVector);
        }
    }
}
