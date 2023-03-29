using System;
using UnityEngine;

public class Crate : Interactible
{
    private Vector3 playerDir;

    public override void Interact()
    {
        base.Interact();
        gameObject.transform.parent = actor.transform;
        playerDir = transform.parent.position - transform.position;
        transform.parent.GetComponent<ThomasCrabController>().canRotate = false;
        transform.parent.GetComponent<ThomasCrabController>().canMoveBackward = true;
        if (Vector3.Angle(Vector3.forward, playerDir) > 45 && Vector3.Angle(Vector3.forward, playerDir)<135)
        {
            transform.parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ
                                                                     | RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            transform.parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX 
                                                                     | RigidbodyConstraints.FreezeRotation;
        }
    }

    public override void StopInteract()
    {
        transform.parent.GetComponent<ThomasCrabController>().canRotate = true;
        transform.parent.GetComponent<ThomasCrabController>().canMoveBackward = false;
        transform.parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX 
                                                                 | RigidbodyConstraints.FreezeRotationZ;
        base.StopInteract();
        gameObject.transform.parent = null;
    }
}
