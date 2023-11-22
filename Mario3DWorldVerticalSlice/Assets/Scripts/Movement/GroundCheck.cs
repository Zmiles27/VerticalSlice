using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundCheck : MonoBehaviour
{
    // Be sure to add the ground check to all the things that should be detected as the ground!

    [SerializeField] float coyoteTime = 0.08f; // Time how long the player ground check is set to false again when leaving the ground
    public bool isGrounded = false; // Variable that is false when not on the ground and true when the check is.

    [SerializeField] UnityEvent onLand; // Called when touching the ground again


    // Check if the ground check is colliding with the ground
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Ground")
        {
            StopAllCoroutines();
            isGrounded = true;

            onLand.Invoke();
        }
    }

    // When the player is leaving the ground
    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Ground")
        {
            StartCoroutine(StartCoyoteTime());
        }
    }

    // Start the time that runs
    IEnumerator StartCoyoteTime()
    {
        yield return new WaitForSeconds(coyoteTime);
        isGrounded = false;
        yield break;
    }



    // Stops the coyote time to avoid a crash when the object is destroyed
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
