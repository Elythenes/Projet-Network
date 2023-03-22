using UnityEngine;

public class Activable : MonoBehaviour
{
    [HideInInspector] public bool isActivated;
    [HideInInspector] public bool isElectrified;

    public virtual void Activate()
    {
        Debug.LogFormat("FROM {0} : Activation", this);
        isActivated = true;
    }

    public virtual void Desactivate()
    {
        Debug.LogFormat("FROM {0} : Desctivation", this);
        isActivated = false;
    }

    public virtual void Electrify()
    {
        isElectrified = true;
    }

    public virtual void DesElectrify()
    {
        isElectrified = false;
    }
}
