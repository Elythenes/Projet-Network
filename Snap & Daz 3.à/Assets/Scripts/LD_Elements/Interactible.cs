using UnityEngine;

public class Interactible : MonoBehaviour
{
    public bool isInteracted;

    protected bool canBeInteracted = true;

    public virtual void Interact()
    {
        isInteracted = !isInteracted;
        canBeInteracted = false;
    }
}
