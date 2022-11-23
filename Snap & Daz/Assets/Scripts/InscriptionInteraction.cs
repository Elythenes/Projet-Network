using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InscriptionInteraction : MonoBehaviour
{
    public Image image;
    private Sprite sprite;
    [HideInInspector] public bool isActivable;
    public GameObject UIInteract;
    private bool imageDisplayed;
    private Rigidbody localPlayerRb;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
        imageDisplayed = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActivable = true;
            UIInteract.SetActive(true);
            localPlayerRb = other.gameObject.GetComponent<Rigidbody>();
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
        if (Input.GetKeyDown(KeyCode.A) && isActivable)
        {
            if (imageDisplayed) 
            { 
                image.gameObject.SetActive(false); 
                imageDisplayed = false;
                localPlayerRb.constraints = RigidbodyConstraints.FreezeRotation;
            }
            else
            {
                image.sprite = sprite;
                image.gameObject.SetActive(true);
                imageDisplayed = true;
                localPlayerRb.constraints = RigidbodyConstraints.FreezeAll;
            }
                
        }
        
    }
}
