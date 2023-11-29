using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    private void Update()
    {
        if(transform.position.y < 0)
        {
            transform.position = new Vector3(-21, 55, -26);
        }
    }
}
