using System;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [Header("Interaction")] 
    [Tooltip("Glissez tous les éléments qui sont activés par le levier")] public List<Activable> activatedElements;

    private int numberOfActors;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Daz") && !other.CompareTag("Interactible")) return;

        numberOfActors++;
        
        foreach (var obj in activatedElements)
        {
            obj.Activate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Daz") && !other.CompareTag("Interactible")) return;

        numberOfActors--;

        if (numberOfActors > 0) return;
        
        foreach (var obj in activatedElements)
        {
            obj.Desactivate();
        }
    }
}
