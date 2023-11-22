using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickup : MonoBehaviour
{
    [SerializeField] int coinValue = 0;
    [SerializeField] int scoreValue = 0;
    private PickupHolder holder;

    [SerializeField] UnityEvent onPickup;



    void Start ()
    {
        GameObject holder_object = GameObject.FindGameObjectWithTag("PickupManager");
        holder = holder_object.GetComponent<PickupHolder>();
    }


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
