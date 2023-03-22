using UnityEngine;

public class Electric : MonoBehaviour
{
    private bool isElectrified;

    public bool IsElectrified
    {
        get => isElectrified;
        set
        {
            isElectrified = value;

            if (isElectrified)
            {
                Electrify();
            }
            else
            {
                DesElectrify();
            }
            
        }
    }

    protected virtual void Electrify()
    {
        
    }

    protected virtual void DesElectrify()
    {
        
    }
    
    
}
