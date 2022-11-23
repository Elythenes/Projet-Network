using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorBehaviour : MonoBehaviour
{
    public Vector3 positionDifferenceWhenOpen;
    public float openingDuration;
    public float closingDuration;
    public float timeMoveCamera;
    private Vector3 positionWhenClosed;
    public bool cinematiquePorte = false;

    private void Start()
    {
        positionWhenClosed = gameObject.transform.position;
    }

    public void OuverturePorte()
    {
        transform.DOMove(transform.position + positionDifferenceWhenOpen, openingDuration);
        if (cinematiquePorte)
        {
            CameraManager.instance.CinematiquePorte(gameObject,timeMoveCamera); 
        }
    }

    public void FermeturePorte()
    {
        transform.DOMove(positionWhenClosed, closingDuration);
    }
}
