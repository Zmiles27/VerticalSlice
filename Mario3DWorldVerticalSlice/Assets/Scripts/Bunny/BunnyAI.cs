using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BunnyAI : MonoBehaviour
{
    public GameObject player;
    public GameObject bunny;

    public Vector3 position;

    public float distance;
    private void Update()
    {
        distance = Vector3.Distance(bunny.transform.position, player.transform.position);
        position = bunny.transform.position - player.transform.position; 

        if (distance <= 4)
        {
            // bunny staat aa rechter kant van water
            if (bunny.transform.position.x >= transform.position.x)
            {
                if (player.transform.position.z < bunny.transform.position.z)
                {
                    Rotation(-1);
                }

                else if (player.transform.position.z > bunny.transform.position.z)
                {
                    Rotation(1);
                }
            }
            // bunny staat aan linker kant van water
            else if (bunny.transform.position.x <= transform.position.x)
            {
                if (player.transform.position.z < bunny.transform.position.z)
                {
                    Rotation(1);
                }

                else if (player.transform.position.z > bunny.transform.position.z)
                {
                    Rotation(-1);
                }
            }
        }
    }
    private void Rotation(float speed)
    {
        float newRot = transform.rotation.eulerAngles.y + speed;
        transform.rotation = Quaternion.Euler(0, newRot, 0);
    }
}
