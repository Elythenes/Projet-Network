using System;
using System.Collections.Generic;
using UnityEngine;

public class PullingHandle : Interactible
{
    private Vector3 startPos;

    [Tooltip("Glissez les divers éléments à déplacer, et pour chacun indiquer la direction dans laquelle il doit se déplacer")]
    public List<PulledElements> pulledElements;

    void Start()
    {
        for (int i = 0; i < pulledElements.Count; i++)
        {
            pulledElements[i].dir = pulledElements[i].direction switch
            {
                Vectors.up => Vector3.up,
                Vectors.down => Vector3.down,
                Vectors.back => Vector3.back,
                Vectors.forward => Vector3.forward,
                Vectors.left => Vector3.left,
                Vectors.right => Vector3.right,
            };
            
            startPos = transform.position;
            
            foreach (var t in pulledElements)
            {
                t.startPos = t.element.transform.position;
            }
        }
    }

    public override void Interact()
    {
        base.Interact();
        gameObject.transform.parent = actor.transform;
    }

    public override void StopInteract()
    {
        base.StopInteract();
        gameObject.transform.parent = null;
    }

    void Update()
    {
        for (int i = 0; i < pulledElements.Count; i++)
        {
            pulledElements[i].element.transform.position = pulledElements[i].startPos + pulledElements[i].dir * Vector3.Distance(transform.position, startPos);
        }

        if (isInteracted) return; // Faire revenir la poignée si elle n'est pas utilisée

        transform.position = Vector3.MoveTowards(transform.position, startPos, Time.deltaTime * 5f);
    }
}

[Serializable]
public class PulledElements
{
    public GameObject element;
    public Vectors direction;
    [HideInInspector] public Vector3 dir;
    [HideInInspector] public Vector3 startPos;
}

[Serializable]
public enum Vectors
{
    up, 
    down,
    left,
    right,
    forward,
    back
}