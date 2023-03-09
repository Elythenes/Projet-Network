using UnityEngine;

public class Interactible : MonoBehaviour
{
    public bool isInteracted;

    [HideInInspector] public GameObject actor;

    public bool canBeInteracted = true;

    public virtual void Interact()
    {
        Debug.Log("Interact");
        isInteracted = !isInteracted;
        canBeInteracted = false;
    }

    public virtual void StopInteract()
    {
        Debug.Log("StopInteract");
        isInteracted = false;
        canBeInteracted = false;
    }
}
