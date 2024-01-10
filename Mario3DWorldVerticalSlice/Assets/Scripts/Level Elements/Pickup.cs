using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickup : MonoBehaviour
{
    [SerializeField] int coinValue = 0; // The value of how many coins this pickup should give!
    [SerializeField] int scoreValue = 0; // Score that this pickup adds when picked up!
    private PickupHolder holder;

    [SerializeField] UnityEvent onPickup; // Called when the pickup is picked up. 


    // Gets the holder that manages score and coins.
    void Start ()
    {
        GameObject holder_object = GameObject.FindGameObjectWithTag("PickupManager");
        holder = holder_object.GetComponent<PickupHolder>();
    }


    // Add score and coins when the player collides with this object
    private void OnTriggerEnter(Collider body)
    {
        if (body.tag == "Player")
        {
            holder.AddValues(coinValue, scoreValue);
            onPickup.Invoke();

            Destroy(gameObject);
        }
    }
}
