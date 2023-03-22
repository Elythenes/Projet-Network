using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThomasCrabController : MonoBehaviour
{
    public Crab activeCrab;
    private bool isSnap;

    public Rigidbody rb;
    
    private LayerMask ground = 3;
    
    private Vector3 move;
    public float speed;

    public bool canMove;
    public bool canRotate;

    public float rotateSpeed;
    
    public GameObject interactibleObject;

    [HideInInspector] public bool canMoveBackward;
    
    public void OnMove(InputAction.CallbackContext ctx)
    {
        var x = ctx.ReadValue<Vector2>().x;
        var z = ctx.ReadValue<Vector2>().y;

        move = new Vector3(x, 0f, z).normalized;
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Interact();
        }

        if (ctx.canceled)
        {
            StopInteract();
        }
    }

    void Start()
    {
        switch (activeCrab)
        {
            case Crab.Snap :
                isSnap = true;
                break;
            case Crab.Daz :
                isSnap = false;
                break;
        }
    }
    
    void Update()
    {
        if (!canMove) return;
        
        if (move!= Vector3.zero)
        {
            var targetRotation = Quaternion.LookRotation(move);

            if (canRotate)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            }
            
            if (canMoveBackward)
            {
                Debug.DrawRay(transform.position, transform.forward * 3, Color.black);
                Debug.DrawRay(transform.position, move * 3, Color.red);
                if (Vector3.Angle(transform.forward, move) < 90)
                {
                    rb.MovePosition(transform.position + transform.forward * move.magnitude * speed * Time.deltaTime);
                }
                else
                {
                    rb.MovePosition(transform.position - transform.forward * move.magnitude * speed * Time.deltaTime);
                }
            }
            else
            {
                rb.MovePosition(transform.position + transform.forward * move.magnitude * speed * Time.deltaTime);
            }
        }
    }

    private void Interact()
    {
        if (interactibleObject == null) return;

        var interactible = interactibleObject.GetComponent<Interactible>();

        if ((interactible.onlyUsableByDaz && isSnap) || (interactible.onlyUsableBySnap && !isSnap)) return;

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
    
    private void OnTriggerEnter(Collider other)
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

[Serializable]
public enum Crab
{
    Snap,
    Daz
}
