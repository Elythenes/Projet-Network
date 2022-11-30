using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushRocher : MonoBehaviour
{
    public bool canActive;
    public bool isPushed;
    public GameObject UIInteract;
    public GameObject daz;
    public DazController dazScript;
    private Vector3 initialScale;
    private Quaternion initialRotation;
    
    private void Awake()
    {
        initialRotation = transform.localRotation;
        initialScale = transform.localScale;
        daz = GameObject.Find("Daz");
        if (daz is not null)
        {
            dazScript = daz.GetComponent<DazController>();
        }
       
    }
    private void Update()
    {
        if (daz is not null)
        {
            transform.localRotation = initialRotation;
        
            if (canActive)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    isPushed = !isPushed;
                }
            }
            else
            {
                transform.parent = null;
                isPushed = false;
                dazScript.canRotate = true;
            }

            if (isPushed)
            {
                transform.localScale = new Vector3 (initialScale.x/daz.transform.localScale.x,initialScale.y/daz.transform.localScale.y,initialScale.z/daz.transform.localScale.z);
                transform.parent = daz.transform;
                dazScript.canRotate = false;
            }
            else
            {
                transform.localScale = initialScale;
                dazScript.canRotate = true;
                transform.parent = null;
            }
        }
      
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            UIInteract.SetActive(true);
            canActive = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            UIInteract.SetActive(false);
            canActive = false;
        }
    }
}
