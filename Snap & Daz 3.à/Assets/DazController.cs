using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DazController : MonoBehaviour
{
    #region Trucs Chelou pour les Input

    private PlayerControls inputAction;
    private PlayerInputManager inputManager;

    private void OnEnable()
    {

        inputAction.Player2.Enable();
    }

    private void OnDisable()
    {

        inputAction.Player2.Disable();
    }

    #endregion

    public Vector3 moveInput;
    public Vector3 moveVector;
    public Vector3 rotationVector;
    public float speed;
    public bool canRotate;
    public float rotateSpeed;
    public Rigidbody rb;
    public LayerMask ground;
    

    void Awake()
    {
        inputAction = new PlayerControls();

        inputAction.Player2.Move.performed += ctx => moveInput = ctx.ReadValue<Vector3>();
        inputAction.Player2.Interact.performed += ctx => Interact();
        inputAction.Player2.Special.performed += ctx => Special();

        inputManager.JoinPlayer(1, 0, "Keyboard2", Keyboard.current);
    }
    

    private void Update()
    {
        Debug.DrawRay(transform.position,transform.forward - transform.up,Color.green);
            
        if (!Physics.Raycast(transform.position, transform.forward - transform.up,ground))
        {
            moveInput.z = Mathf.Clamp(moveInput.y, -99, 0);
            rb.velocity = Vector3.zero;
        }
        
    
        moveVector = new Vector3(-moveInput.x, 0,  moveInput.y);
        rotationVector.Normalize();
        
        if (rotationVector != Vector3.zero)
        {
            rb.velocity += (rotationVector * (speed * Time.deltaTime));
            if (canRotate)
            {
                Quaternion rotateTo = Quaternion.LookRotation(rotationVector,Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation,rotateTo,rotateSpeed * Time.deltaTime);
            }
                
        }
        else if(rotationVector.x != 0 && rotationVector.y != 0)
        {
            rb.velocity += (rotationVector * speed/2 * Time.deltaTime);
        }
    }

    public void Interact()
    {

    }

    public void Special()
    {

    }

}

