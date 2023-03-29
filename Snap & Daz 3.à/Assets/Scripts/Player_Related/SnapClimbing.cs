using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapClimbing : MonoBehaviour
{
    [Header("Stats")]
    public float rotationSpeed;

    [Header("References")] private Rigidbody rb;
 
    private ThomasCrabController controller;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<ThomasCrabController>();
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 1.5f,Color.yellow,0.5f);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit,1.5f)) // si snap rencontre un mur
        {

            if (hit.collider.gameObject.transform.position.y > transform.position.y)// si snap veut monter
            {
                Quaternion desiredRotation = Quaternion.Euler(-90, 0, 0);
                rb.useGravity = false;
                if (controller.move.z > 0)
                {
                    controller.isClimbing = true;
                    transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
                }
            }
            else
            {
                Quaternion desiredRotation = Quaternion.Euler(90, 0, 0);
                rb.useGravity = false;
                if (controller.move.z > 0)
                {
                    controller.isClimbing = true;
                    transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
                }
            }
         
        }
        
        if (controller.isClimbing && Physics.Raycast(transform.position, transform.forward - transform.up, 2f,7)) // si snap quitte un mur
        {
            Quaternion desiredRotation = Quaternion.Euler(0, 0, 0);
            rb.useGravity = true;
            controller.isClimbing = false;
            if (controller.move.z > 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
}
