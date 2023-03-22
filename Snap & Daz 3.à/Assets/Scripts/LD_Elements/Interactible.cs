using System;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    [Tooltip("Détermine qui peut utiliser l'objet")] public Actors usableBy;
    
    [HideInInspector] public bool canBeInteracted = true;
    
    [HideInInspector] public bool isInteracted;

    [HideInInspector] public GameObject actor;

    [HideInInspector] public bool onlyUsableByDaz;
    [HideInInspector] public bool onlyUsableBySnap;

    private void Start()
    {
        switch (usableBy)
        {
            case Actors.Both:
                break;
            case Actors.Snap:
                onlyUsableBySnap = true;
                break;
            case Actors.Daz:
                onlyUsableByDaz = true;
                break;
        }
    }
    
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

[Serializable]
public enum Actors
{
    Both,
    Snap,
    Daz
}
