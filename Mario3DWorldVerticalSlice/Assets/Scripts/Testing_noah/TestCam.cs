using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCam : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset = Vector3.zero;


    private void Update()
    {
        Vector3 newPos = target.position + offset;
        transform.position = newPos;
    }
}
