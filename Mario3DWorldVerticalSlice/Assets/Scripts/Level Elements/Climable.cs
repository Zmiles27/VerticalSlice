using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climable : MonoBehaviour
{
    [SerializeField] Vector3 endPos; // Final position of the climable object
    [SerializeField] Vector3 startPos; // The lowest position of the climable object

    const float debugSphereRadius = 0.5f; // The radius of the sphere used for debugging


    
    // When the player collides with this climable WHILE in the air, the player will start to climb
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is the player
        if (collision.gameObject.tag == "Player")
        {
            GameObject player = collision.gameObject;
            GroundCheck playerGroundCheck;

            // Check if the player has a groundcheck, if so, get the groundcheck's reference
            if (player.GetComponentInChildren<GroundCheck>() == true)
            {
                playerGroundCheck = player.GetComponentInChildren<GroundCheck>();
            }
            else
            {
                return;
            }

            // If the player isn't grounded, set the players current climbing target to this climable
            if (playerGroundCheck != null && playerGroundCheck.isGrounded == false)
            {
                PlayerStateMachine stateMachine = collision.gameObject.GetComponent<PlayerStateMachine>();
                Climbing playerClimbing = collision.gameObject.GetComponent<Climbing>();

                stateMachine.CURRENTSTATE = PlayerStateMachine.PlayerState.CLIMBING;
                playerClimbing.SetClimbingCenter(this);
            }
        }
    }



    // Debugging
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawLine(transform.position + startPos, transform.position + endPos);
        Gizmos.DrawSphere(transform.position + endPos, debugSphereRadius);
    }


    // Returns the final position of the climable
    public Vector3 GetEndPos()
    {
        Vector3 globalEndPos = transform.position + endPos;
        return globalEndPos;
    }
}
