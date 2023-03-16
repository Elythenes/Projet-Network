using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrabController : MonoBehaviour
{
    private LayerMask ground = 3;
    private Rigidbody rb;
    
    
    [Header("Metrics")]
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;
    [SerializeField] float gravityScale;

    [Header("States et Références")] 
    public Vector3 moveVector;
    public bool isSnap;
    public bool interacting;
    public bool specialing;
    public bool canRotate = true;
    public bool canMove = true;
    public GameObject interactibleObject;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * (gravityScale * 9.81f),ForceMode.Acceleration);
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward - transform.up, Color.green);

        if (!Physics.Raycast(transform.position, transform.forward - transform.up, ground))
        {
            moveVector.z = Mathf.Clamp(moveVector.y, -99, 0);
            rb.velocity = Vector3.zero;
        }
        
        if (canRotate && moveVector != Vector3.zero)
        {
            Quaternion rotateTo = Quaternion.LookRotation(moveVector, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateTo, rotateSpeed * Time.deltaTime);
        }

        if (canMove)
        {
            rb.velocity = moveVector * (speed*500 * Time.deltaTime);
        }
        else if (moveVector.x != 0 && moveVector.y != 0 && canMove)
        {
            rb.velocity += (moveVector * speed / 2 * Time.deltaTime);
        }
    }

    private void OnMove(InputValue value)
    {
        moveVector = value.Get<Vector3>();
    }
    
    private void OnInteract(InputValue value)
    {
        if (value.Get<float>() <= 0)
        {
            StopInteract();
        }
        if(value.Get<float>() < 1) return;
        Interact();
    }
    
    private void OnSpecial(InputValue value)
    {
        if(value.Get<float>() < 1) return;
        if (!isSnap)
        {
            GetComponent<CoupDePince>().Coup();
        }
    }
    
    
    private void Interact()
    {
        if (interactibleObject == null) return;

        var interactible = interactibleObject.GetComponent<Interactible>();

        interactible.actor = gameObject;
        interactible.Interact();
    }

    private void StopInteract()
    {
        if (interactibleObject == null) return;

        var interactible = interactibleObject.GetComponent<Interactible>();

        interactible.actor = null;
        interactible.StopInteract();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactible"))
        {
            interactibleObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactible"))
        {
            interactibleObject = null;
        }
    }
    
    
    
}
