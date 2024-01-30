using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    // Script for entities that get destroyed once attacked

    [SerializeField] int team = 0; // This entity's team
    [SerializeField] UnityEvent onDie; // Invoked when dying


    // Handle a hit
    public void HandleHit(int hitTeam)
    {
        if (hitTeam != team)
        {
            Die();
        }
    }


    // Call onDie and destroy the object
    void Die()
    {
        onDie.Invoke();

        Destroy(gameObject);
    }
}
