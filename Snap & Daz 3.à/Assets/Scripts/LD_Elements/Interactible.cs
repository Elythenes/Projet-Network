using UnityEngine;

public class Interactible : MonoBehaviour
{
    [HideInInspector] public bool isInteracted;

    [HideInInspector] public GameObject actor;

    protected bool canBeInteracted = true;

    public virtual void Interact()
    {
        isInteracted = !isInteracted;
        canBeInteracted = false;
    }

    public virtual void StopInteract()
    {
        isInteracted = false;
        canBeInteracted = false;
    }
}
