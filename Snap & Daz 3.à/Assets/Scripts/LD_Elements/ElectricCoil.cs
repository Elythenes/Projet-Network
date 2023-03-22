using System.Collections.Generic;
using UnityEngine;

public class ElectricCoil : Electric
{
    [Tooltip("Glissez tous les éléments qui sont bloqués par la bobine")] public List<Activable> activatedElements;
    
    protected override void Electrify()
    {
        base.Electrify();
        
        foreach (var obj in activatedElements)
        {
            obj.Electrify();
        }
    }

    protected override void DesElectrify()
    {
        base.Electrify();
        
        foreach (var obj in activatedElements)
        {
            obj.DesElectrify();
        }
    }
}
