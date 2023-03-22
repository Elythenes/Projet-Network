using UnityEngine;
using System.Collections.Generic;

public class Generator : Electric
{
    //TEMPORAIRE POUR DEBUG
    public Material blueMaterial;
    public Material yellowMaterial;
    
    [Tooltip("Glissez tous les éléments qui sont activés par le générateur")] public List<Activable> activatedElements;
    
    protected override void Electrify()
    {
        base.Electrify();
        
        foreach (var obj in activatedElements)
        {
            obj.Activate();
        }

        GetComponent<Renderer>().material = yellowMaterial;
    }

    protected override void DesElectrify()
    {
        base.Electrify();
        
        foreach (var obj in activatedElements)
        {
            obj.Desactivate();
        }

        GetComponent<Renderer>().material = blueMaterial;
    }
}
