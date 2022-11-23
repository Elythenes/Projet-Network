using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevierDAZ : MonoBehaviour
{
    public GameObject UIInteract;
    public bool isActive;
    public bool canActive;

    public ElectriqueSNAP electriqueAssocie;
    public GameObject plateformeMAIN;
    private MeshRenderer plateformeMAINrenderer;
    public GameObject plateforme2;
    public float plateformeSpeed;
    public float maxY;
    public float minY;

    private void Start()
    {
        plateformeMAINrenderer = plateformeMAIN.GetComponent<MeshRenderer>();
        maxY += plateformeMAIN.transform.position.y;
        minY -= plateformeMAIN.transform.position.y;
    }

    private void Update()
    {

        if (canActive)
        {
            if (Input.GetKey(KeyCode.F))
            {
                isActive = true; 
            }
            else
            {
                isActive = false;
            }
        }
        else
        {
            isActive = false;
        }


        if (!electriqueAssocie.isLocked)
        {
            if (isActive)
            {
                if (plateformeMAIN.transform.position.y < maxY)
                {
                    plateformeMAIN.transform.Translate(new Vector3(0,plateformeSpeed,0));
                    plateforme2.transform.Translate(new Vector3(0,-plateformeSpeed,0));
                }
            }
            else
            {
                if (plateformeMAINrenderer.bounds.max.y > minY)
                {
                    plateformeMAIN.transform.Translate(new Vector3(0,-plateformeSpeed,0));
                    plateforme2.transform.Translate(new Vector3(0,plateformeSpeed,0));
                }
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
