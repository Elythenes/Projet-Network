using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thomas_CameraController : MonoBehaviour
{
    private GameObject snap;
    private GameObject daz;
    private Camera cam;

    public Vector3 offset;
    public float sensitivity;

    void Start()
    {
        snap = GameObject.FindGameObjectWithTag("Snap");
        daz = GameObject.FindGameObjectWithTag("Daz");
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        var target = snap.transform.position + (daz.transform.position - snap.transform.position) / 2 + offset;
        
        transform.position = Vector3.Slerp(transform.position, target, sensitivity * Time.deltaTime);
    }
    
}
