using UnityEngine;

public class Activable : MonoBehaviour
{
    public bool isActivated;

    public virtual void Activate()
    {
        isActivated = true;
    }

    public virtual void Desactivate()
    {
        isActivated = false;
    }
}
