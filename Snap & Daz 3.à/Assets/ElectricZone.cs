using System.Collections.Generic;
using UnityEngine;

public class ElectricZone : MonoBehaviour
{
    public List<GameObject> electrifiedElements;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Electric"))
        {
            other.GetComponent<Electric>().IsElectrified = true;
            electrifiedElements.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) //Pas forcément utilisée si le joueur ne peut pas bouger
    {
        if (other.CompareTag("Electric"))
        {
            other.GetComponent<Electric>().IsElectrified = false;
            electrifiedElements.Remove(other.gameObject);
        }
    }

    public void DisableElements()
    {
        foreach (var obj in electrifiedElements)
        {
            obj.GetComponent<Electric>().IsElectrified = false;
        }
        electrifiedElements.Clear();
    }
}
