using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;


public class BoutonBehaviour : MonoBehaviour
{
    public bool isActivable;
    public GameObject imageActiv;
    
    [Header("Characteristiques de la porte associée")]
    public GameObject AssociatedDoor;
    public float HauteurOuverture;
    public float DureeOuverture;
    public float TimeMoveCamera;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActivable = true;
            imageActiv.SetActive(true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActivable = false;
            imageActiv.SetActive(false);
        }
    }

    private void Update()
    {
        if (isActivable)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Bouton();
            }
        }
    }


    void Bouton()
    {
        AssociatedDoor.transform.DOMove(AssociatedDoor.transform.position + new Vector3(0,HauteurOuverture,0), DureeOuverture);
        CameraManager.instance.CinematiquePorte(AssociatedDoor,TimeMoveCamera);
    }

}
