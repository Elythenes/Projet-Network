using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;


public class BoutonBehaviour : MonoBehaviour
{
    [Header("Caracteristiques du bouton")]
    [HideInInspector] public bool isActivable;
    private bool lockActivation;
    public bool OneUse;
    public GameObject UIInteract;
    public UnityEvent EventActivation;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !lockActivation)
        {
            isActivable = true;
            UIInteract.SetActive(true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActivable = false;
            UIInteract.SetActive(false);
        }
    }
    
    private void Update()
    {
        if (isActivable)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                EventActivation.Invoke();
                if (OneUse)
                {
                    lockActivation = true;
                    isActivable = false;
                }
            }
        }
    }

}
