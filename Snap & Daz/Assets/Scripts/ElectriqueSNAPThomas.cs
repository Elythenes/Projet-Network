using System;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class ElectriqueSNAPThomas : ActivatedElements
{
    [SerializeField] private GameObject _UIInteract;

    public UnityEvent electricBehaviour;
    
    public ActivatedElements activatedElements;

    public PhotonView[] photonViews;

    private void Start()
    {
        activatedElements = GetComponent<ActivatedElements>();
    }

    void Update()
    {
        if (isActivated)
        {
            foreach (var photonView in photonViews)
            {
                photonView.RequestOwnership();
            }
            
            electricBehaviour.Invoke();
            isActivated = false;
        }
    }

    public void OnTriggerEnter(Collider other) //Détecte SNAP quand il est proche
    {
        if (other.gameObject.layer == 7)
        {
            _UIInteract.SetActive(true);
            
            other.GetComponent<SnapController>().activatedElements = activatedElements;
        }
    } 

    public void OnTriggerExit(Collider other) //Détecte SNAP quand il part
    {
        if (other.gameObject.layer == 7)
        {
            _UIInteract.SetActive(false);
            
            other.GetComponent<SnapController>().activatedElements = null;
        }
    }
}
