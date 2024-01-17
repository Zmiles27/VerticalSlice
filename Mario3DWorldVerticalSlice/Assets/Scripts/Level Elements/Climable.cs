using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climable : MonoBehaviour
{
    [SerializeField] Vector3 endPos;
    [SerializeField] Vector3 startPos;

    const float debugSphereRadius = 0.5f;




    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerStateMachine stateMachine = collision.gameObject.GetComponent<PlayerStateMachine>();
            Climbing playerClimbing = collision.gameObject.GetComponent<Climbing>();

            stateMachine.CURRENTSTATE = PlayerStateMachine.PlayerState.CLIMBING;
            playerClimbing.SetClimbingCenter(this);
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawLine(transform.position + startPos, transform.position + endPos);
        Gizmos.DrawSphere(transform.position + endPos, debugSphereRadius);
    }


    public Vector3 GetEndPos()
    {
        Vector3 globalEndPos = transform.position + endPos;
        return globalEndPos;
    }
}
