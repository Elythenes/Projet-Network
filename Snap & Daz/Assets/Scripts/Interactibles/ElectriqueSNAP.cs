using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectriqueSNAP : MonoBehaviour
{
    public bool canActive;
    public bool isLocked;
    public GameObject UIInteract;
    
    private void Update()
    {

        if (canActive)
        {
            if (Input.GetKey(KeyCode.F))
            {
                isLocked = true;
            }
            else
            {
                isLocked = false;
            }
        }
        else
        {
            isLocked = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Debug.Log("oui");
            UIInteract.SetActive(true);
            canActive = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            UIInteract.SetActive(false);
            canActive = false;
        }
    }
}
