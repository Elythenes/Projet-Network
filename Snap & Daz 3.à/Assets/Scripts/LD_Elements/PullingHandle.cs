using System.Collections.Generic;
using UnityEngine;

public class PullingHandle : Interactible
{
    public List<GameObject> pulledElements;
    [HideInInspector] public List<Vector3> startingPos;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;

        foreach (var t in pulledElements)
        {
            startingPos.Add(t.transform.position);
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
            pulledElements[i].transform.position = startingPos[i] + Vector3.up * Vector3.Distance(transform.position, startPos);
        }

        if (isInteracted) return; // Faire revenir la poignée si elle n'est pas utilisée

        transform.position = Vector3.MoveTowards(transform.position, startPos, Time.deltaTime * 5f);
    }
}
