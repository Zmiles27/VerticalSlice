using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityRespawn : MonoBehaviour
{
    [SerializeField] Vector3 spawnPoint;
    public float threshold;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < threshold)
        {
            transform.position = spawnPoint;
        }

    }
}
