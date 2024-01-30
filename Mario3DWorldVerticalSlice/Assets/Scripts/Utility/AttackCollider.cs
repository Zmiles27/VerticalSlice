using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class AttackCollider : MonoBehaviour
{
    // Used for attacking other objects.
    // Calls the HandleHit function in the enemy. MAKE SURE THE ENEMY HAS THIS FUNCTION AND A DIFFERENT TEAM VARIABLE

    [SerializeField] int team = 0; // Can only damage an entity with a different team
    public bool isActive = false; // Can only damage when active

    BoxCollider trigger; // collider of the attack collider


    // Set up
    private void Start()
    {
        trigger = GetComponent<BoxCollider>();
    }


    // Check if active and then damage entity
    private void OnTriggerEnter(Collider body)
    {
        if (isActive == true)
        {
            if (body.GetComponent<Entity>() != null)
            {
                Entity entity = body.GetComponent<Entity>();
                entity.HandleHit(team);
            }
        }
    }


    // Debugging
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (isActive == true)
        {
            Gizmos.DrawCube(transform.position, trigger.size);
        }
    }
}
