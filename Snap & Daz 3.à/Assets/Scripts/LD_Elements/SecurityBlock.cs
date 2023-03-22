using System.Collections.Generic;
using UnityEngine;

public class SecurityBlock : MonoBehaviour
{
    [HideInInspector] public List<GameObject> objectsUnder;

    private MovingElement mEl;

    void Start()
    {
        mEl = GetComponent<MovingElement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Snap") || other.CompareTag("Daz") || other.CompareTag("Interactible"))
        {
            if (other.gameObject.transform.position.y > transform.position.y) return;
            
            objectsUnder.Add(other.gameObject);

            mEl.cantMove = true;
            mEl.StopCo();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Snap") || other.CompareTag("Daz") || other.CompareTag("Interactible"))
        {
            if (other.gameObject.transform.position.y > transform.position.y) return;
            
            objectsUnder.Remove(other.gameObject);

            if (objectsUnder.Count == 0)
            {
                mEl.cantMove = false;
                mEl.Desactivate();
            }
        }
    }
}
