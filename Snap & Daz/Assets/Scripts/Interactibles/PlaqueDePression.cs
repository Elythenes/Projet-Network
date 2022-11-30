using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class PlaqueDePression : MonoBehaviour
{
    [Header("Caractéristiques de la plaque")]
    private MeshRenderer meshRenderer;
    [SerializeField] private Material buttonMaterial;
    [SerializeField] private Material activatedMaterial;
    public UnityEvent eventActivation;
    public UnityEvent eventDesactivation;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7 || other.gameObject.layer == 8 || other.gameObject.layer == 9)
        {
            meshRenderer.material = activatedMaterial;
            eventActivation.Invoke();
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7 || other.gameObject.layer == 8 || other.gameObject.layer == 9)
        {
            meshRenderer.material = buttonMaterial;
            eventDesactivation.Invoke();
        }
    }
}
