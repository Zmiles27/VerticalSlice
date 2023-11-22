using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Vector3 velocity = Vector3.zero;

    Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        float zInput = Input.GetAxisRaw("Vertical");
        float xInput = Input.GetAxisRaw("Horizontal");
        velocity = new Vector3(xInput, 0, zInput) * speed * Time.deltaTime;

        if (velocity != Vector3.zero)
        {
            rb.MovePosition(transform.position + velocity);
        }
    }
}
