using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    Mover mover;
    Rigidbody rb;
    PlayerStateMachine stateMachine;

    [SerializeField] GroundCheck groundCheck;

    Climable currentClimable;

    [SerializeField] float climbJumpPower = 600.0f;
    [SerializeField] float climbSpeed = 10.0f;

    public int gamepad = 0; // Used gamepad



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

            if (zInput != 0 && transform.position.y < currentClimable.GetEndPos().y)
            {
                mover.Yvelocity = zInput * climbSpeed * Time.deltaTime;
            }
            else
            {
                mover.Yvelocity = 0;
            }

            if (transform.position.y >= currentClimable.GetEndPos().y)
            {
                mover.MoveToPosition(currentClimable.GetEndPos());
            }
        }

        if (groundCheck.isGrounded ==  true || Input.GetButton("Jump" + gamepad.ToString()))
        {
            Vector3 jumpVector = new Vector3(0, climbJumpPower * Time.deltaTime, 0);
            rb.AddForce(jumpVector, ForceMode.Impulse);

            rb.useGravity = true;
            stateMachine.CURRENTSTATE = PlayerStateMachine.PlayerState.NORMAL;
        }
    }



    public void SetClimbingCenter(Climable climable)
    {
        currentClimable = climable;
    }
}
