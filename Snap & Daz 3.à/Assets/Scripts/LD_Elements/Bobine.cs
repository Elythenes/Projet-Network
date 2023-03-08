using UnityEngine;

public class Bobine : Electric
{
    public Material blueMaterial;
    public Material yellowMaterial;
    
    protected override void Electrify()
    {
        base.Electrify();

        GetComponent<Renderer>().material = yellowMaterial;
    }

    protected override void DesElectrify()
    {
        base.Electrify();

        GetComponent<Renderer>().material = blueMaterial;
    }
}
