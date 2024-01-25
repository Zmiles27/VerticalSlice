
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class CameraScript : MonoBehaviour
{
    public List<Transform> targets;
    public Vector3 _offset;
    public float smoothTime = .3f;
    private Vector3 _currentVelocity = Vector3.zero;
    public float minZoom = 50f;
    public float maxZoom = 20f;
    public float zoomLimiter = 50f;
    private Camera cam;

    
    void Start()
    {
        cam = GetComponent<Camera>();
    }
    
    private void LateUpdate()
    {
        if (targets.Count == 0) return;

        Move();
        Zoom();
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);  
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + _offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref _currentVelocity, smoothTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 1; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }
    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }

}
