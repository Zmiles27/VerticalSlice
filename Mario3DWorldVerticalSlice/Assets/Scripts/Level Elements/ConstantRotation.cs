using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;

    private void FixedUpdate()
    {
        float newRot = transform.rotation.eulerAngles.y + speed;
        transform.rotation = Quaternion.Euler(0, newRot, 0);
    }
}
