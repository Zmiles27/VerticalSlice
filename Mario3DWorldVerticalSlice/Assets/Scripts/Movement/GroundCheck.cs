using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;


public class GroundCheck : MonoBehaviour
{
    // Be sure to add the ground check to all the things that should be detected as the ground!

    [SerializeField] float groundRayLength = 1.3f; // Length of the ground raycast
    [SerializeField] float exitRayLength = 1.5f; // Length of the raycast to check if the player really left the ground
    [SerializeField] float coyoteTime = 0.08f; // Time how long the player ground check is set to false again when leaving the ground
    private bool triggerIsDetecting = false; // Variable that is false when the trigger is not detecting anything and vice versa
    public bool isGrounded = false; // Variable that is false when not on the ground and true when the check is.

    [SerializeField] UnityEvent onLand; // Called when touching the ground again
    [SerializeField] UnityEvent onAir; // Called when touching the ground again

    // Debugging colors
    private Color groundRayColor = Color.cyan;
    private Color exitRayColor = new Color(0, 0, 1, 0.25f);



    // Check if the ground check is colliding with the ground
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Ground")
        {
            StopAllCoroutines();
            triggerIsDetecting = true;
        }
    }

    // When the player is leaving the ground
    private void OnTriggerExit(Collider collider)
    {
        // The raycast is for checking if we really left the ground
        if (collider.tag == "Ground")
        {
            StartCoroutine(StartCoyoteTime());
        }
    }

    // Start the timer that runs coyote time
    IEnumerator StartCoyoteTime()
    {
        yield return new WaitForSeconds(coyoteTime);
        triggerIsDetecting = false;
        yield break;
    }


    // Checks realtime if there is ground with the help of a raycast together with the trigger
    private void FixedUpdate()
    {
        RaycastHit hit; // Result of the raycast

        // if the raycast or trigger is not detecting any ground, then isGrounded is false. Else it is true (Set anything the raycast should ignore on the "Ignore Raycast" Layer).
        if ((Physics.Raycast(transform.position, Vector3.down, out hit, groundRayLength) == true && hit.collider.tag == "Ground") || triggerIsDetecting == true)
        {
            if (isGrounded == false)
            {
                onLand.Invoke();
            }

            isGrounded = true;
        }
        else 
        { 
            isGrounded = false;

            if (Physics.Raycast(transform.position, Vector3.down, exitRayLength) == false)
            {
                onAir.Invoke();
            }
        }
    }



    // Stops the coyote time to avoid a crash when the object is destroyed
    private void OnDestroy()
    {
        StopAllCoroutines();
    }



    // Draw gizmo for debugging
    private void OnDrawGizmos()
    {
        Gizmos.color = exitRayColor;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * exitRayLength);

        Gizmos.color = groundRayColor;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundRayLength);
    }
}
