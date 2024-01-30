using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    // Script for moving the rigidbody of the player

    public float Xvelocity = 0;
    public float Yvelocity = 0;
    public float Zvelocity = 0;
    Vector3 velocity = Vector3.zero;

    Rigidbody rb;


    private void Start()
    {
       rb = GetComponent<Rigidbody>(); 
    }

    private void FixedUpdate()
    {
        velocity = new Vector3(Xvelocity, Yvelocity, Zvelocity);
        rb.MovePosition(transform.position + velocity);
    }

    public void MoveToPosition(Vector3 newPos)
    {
        rb.MovePosition(newPos);
    }
}
