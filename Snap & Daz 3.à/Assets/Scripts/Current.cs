using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Current : MonoBehaviour
{
    public Transform currentOrigin;
    public Transform currentEnd;
    private Vector3 currentDirection;
    private Rigidbody snapRB;
    public float currentForce = 1;
    private bool affectedByCurrent = true;

    private void Start()
    {
        float x = currentEnd.transform.position.x - currentOrigin.transform.position.x;
        float y = currentEnd.transform.position.y - currentOrigin.transform.position.y;
        float z = currentEnd.transform.position.z - currentOrigin.transform.position.z;
        currentDirection = new Vector3(x, y, z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Snap"))
        {
            snapRB = other.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Snap"))
        {
            RaycastHit raycastHit;
            float distance = Vector3.Distance(currentOrigin.position, other.transform.position);
            if (Physics.Raycast(other.transform.position, - currentDirection, out raycastHit, distance, LayerMask.GetMask("BlockWater")))
            {
                Debug.Log("hit caisse");
                return;
            }
            else
            {
                Debug.Log("affected by current");
                snapRB.AddForce(currentDirection*currentForce);
            }
            
            Debug.DrawRay(other.transform.position, - currentDirection * distance, Color.red);
        }
    }
}
