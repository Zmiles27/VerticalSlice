using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dive : MonoBehaviour
{

    [SerializeField] float diveSpeed;
    [SerializeField] float diveTime;
    [SerializeField] float bufferTime;

    [SerializeField] GroundCheck groundCheck; // Check to check if is on ground
    [SerializeField] UnityEvent onDive;

    Rigidbody rb;
    public int gamepad = 0;

    Vector3 inputVector = Vector3.zero;
    bool canDive = true;
    bool bufferingVelocity = false;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }



    private void Update()
    {
        if (Input.GetButtonDown("Dive" + gamepad.ToString()) && groundCheck.isGrounded == false && canDive == true)
        {
            float zInput = Input.GetAxisRaw("Vertical" + gamepad.ToString());
            float xInput = Input.GetAxisRaw("Horizontal" + gamepad.ToString());
            inputVector = new Vector3(xInput, 0, zInput);

            if (inputVector != Vector3.zero) 
            {
                canDive = false;
                StartCoroutine(DiveLoop());

                onDive.Invoke();
            }
        }

        if (groundCheck.isGrounded == true)
        {
            if (canDive == true)
            {
                if (bufferingVelocity == false)
                {
                    rb.velocity = Vector3.zero;
                }
            }
            else StartCoroutine(VelocityBuffer());

            canDive = true;
        }
    }


    IEnumerator DiveLoop()
    {
        rb.useGravity = false;
        yield return new WaitForSeconds(diveTime);

        rb.useGravity = true;
        if (rb.useGravity)
        {
            rb.AddForce(inputVector * diveSpeed, ForceMode.Impulse);
            rb.AddForce(Physics.gravity * (rb.mass * diveSpeed / 4), ForceMode.Impulse);
        }

        yield break;
    }


    IEnumerator VelocityBuffer()
    {
        bufferingVelocity = true;
        yield return new WaitForSeconds(bufferTime);
        bufferingVelocity = false;
        yield break;
    }
}
