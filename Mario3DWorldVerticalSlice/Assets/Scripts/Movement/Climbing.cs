using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Climbing : MonoBehaviour
{
    Mover mover;
    Rigidbody rb;
    PlayerStateMachine stateMachine;

    [SerializeField] GroundCheck groundCheck;

    Climable currentClimable;

    [SerializeField] float climbJumpPower = 600.0f;
    [SerializeField] float climbSpeed = 10.0f;
    [SerializeField] float turnSpeed = 180f;

    [SerializeField] UnityEvent onClimbing; // Invoked when while climbing
    [SerializeField] UnityEvent onReachEnd; // Invoked when while climbing
    [SerializeField] UnityEvent onAction; // Invoked when jumping or landing

    public int gamepad = 0; // Used gamepad

    [SerializeField] Transform playerModel; // Transform of the player model




    private void Start()
    {
        mover = GetComponent<Mover>();
        rb = GetComponent<Rigidbody>();
        stateMachine = GetComponent<PlayerStateMachine>();
    }



    private void FixedUpdate()
    {
        if (currentClimable != null)
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;

            float zInput = Input.GetAxisRaw("Vertical" + gamepad.ToString());
            float xInput = Input.GetAxisRaw("Horizontal" + gamepad.ToString());


            // Up-down movement
            if (xInput == 0 && zInput != 0 && transform.position.y < currentClimable.GetEndPos().y)
            {
                mover.Yvelocity = zInput * climbSpeed * Time.deltaTime;
            }
            else
            {
                mover.Yvelocity = 0;
            }

            if (transform.position.y >= currentClimable.GetEndPos().y)
            {
                Vector3 endPos = Vector3.MoveTowards(transform.position, currentClimable.GetEndPos(), climbSpeed * Time.deltaTime);
                mover.MoveToPosition(endPos);

                onReachEnd.Invoke();
            }
            else
            {
                onClimbing.Invoke();
            }

            // Rotation movement
            if (xInput != 0 && transform.position.y < currentClimable.GetEndPos().y)
            {
                // Get the climable position
                Transform climableTrans = currentClimable.transform;
                Vector3 climablePos = new Vector3(climableTrans.position.x, rb.position.y, climableTrans.position.z);
                
                // Get the next angle
                float angle = -xInput * turnSpeed * Time.deltaTime;

                // Move the player position
                transform.RotateAround(climablePos, Vector3.up, angle);
            }
        }

        // Jumping and leaving the climable
        if (groundCheck.isGrounded ==  true || Input.GetButton("Jump" + gamepad.ToString()))
        {
            Vector3 jumpVector = new Vector3(0, climbJumpPower * Time.deltaTime, 0);
            rb.AddForce(jumpVector, ForceMode.Impulse);

            currentClimable = null;

            rb.useGravity = true;
            stateMachine.CURRENTSTATE = PlayerStateMachine.PlayerState.NORMAL;

            onAction.Invoke();
        }
    }


    
    // Setting the current climable
    public void SetClimbingCenter(Climable climable)
    {
        currentClimable = climable;
    }



    // Debugging
    private void OnDrawGizmos()
    {
        if (currentClimable != null)
        {
            Transform climableTrans = currentClimable.transform;
            Vector3 climablePos = new Vector3(climableTrans.position.x, rb.position.y, climableTrans.position.z);
            float radius = Vector3.Distance(rb.position, climablePos);

            Gizmos.DrawWireSphere(climablePos, radius);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(rb.position, climablePos);
        }
    }
}
