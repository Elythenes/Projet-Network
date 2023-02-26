using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    
    
    [Header("Mouvement")]
    public Vector3 offset;
    public Vector3 farOffset;
    public Quaternion nearRotation;
    public Quaternion farRotation;
    
    [Header("Zoom")]
    public float minZoom;
    public float maxZoom;
    private float zoomController = 15;
    
    [Header("Utilitaire")]
    public float SmoothMoveFactor;
    public float SmoothRotateFactor;
    public float farNearDistance;
    private bool farCam;
    public List<Transform> targets;
    private Vector3 velocity;
    private Camera camera;

    private void Start()
    {
        offset = transform.position - GetCenterPoint();
        camera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
       
        Move();
        Zoom();
        
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(minZoom, maxZoom, GetDistance() / zoomController);
        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, newZoom, Time.deltaTime);
        
        if (GetDistance() >= farNearDistance)
        {
            farCam = true;
        }
        else
        {
            farCam = false;
        }
    }
    
    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        if (farCam)
        {
            Vector3 newPosition = centerPoint + farOffset;
            transform.localPosition = Vector3.SmoothDamp(transform.position,newPosition,ref velocity,SmoothMoveFactor);
            transform.rotation = Quaternion.Slerp(transform.rotation, farRotation, Time.deltaTime/SmoothRotateFactor);
        }
        else
        {
            Vector3 newPosition = centerPoint + offset;
            transform.localPosition = Vector3.SmoothDamp(transform.position,newPosition,ref velocity,SmoothMoveFactor);
            transform.rotation = Quaternion.Slerp(transform.rotation, nearRotation,Time.deltaTime/ SmoothRotateFactor);
        }

    }

    float GetDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }
    
    Vector3 GetCenterPoint()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }
}
