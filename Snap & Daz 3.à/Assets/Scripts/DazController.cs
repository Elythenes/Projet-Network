using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DazController : MonoBehaviour
{
    #region Trucs Chelou pour les Input

  //  private PlayerControls inputAction;
    private PlayerInputManager inputManager;

    private void OnEnable()
    {

       // inputAction.Player.Enable();
    }

    private void OnDisable()
    {

       // inputAction.Player.Disable();
    }

    #endregion

    public Vector3 moveInput2;
    private Vector3 moveVector;
    public Vector3 rotationVector;
    public Rigidbody rb;
    public LayerMask ground;
    public float gravityScale;
    public float globalGravity;
    public float speed;
    public bool canRotate;
    public float rotateSpeed;
    

    void Awake()
    {
       // inputAction = new PlayerControls();

       // inputAction.Player.Move.performed += ctx => moveInput2 = ctx.ReadValue<Vector3>();
        //inputAction.Player.Interact.performed += ctx => Interact();
        //inputAction.Player.Special.performed += ctx => Special();

        //inputManager.JoinPlayer(1, 0, "Keyboard2", Keyboard.current);
    }

   /* private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * (gravityScale * globalGravity),ForceMode.Acceleration);
    }*/

    private void Update()
    {
        Debug.DrawRay(transform.position,transform.forward - transform.up,Color.green);
            
        if (!Physics.Raycast(transform.position, transform.forward - transform.up,ground))
        {
            moveInput2.z = Mathf.Clamp(moveInput2.y, -99, 0);
            rb.velocity = Vector3.zero;
        }
        
    
        moveVector = new Vector3(-moveInput2.x, 0,  moveInput2.y);
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

