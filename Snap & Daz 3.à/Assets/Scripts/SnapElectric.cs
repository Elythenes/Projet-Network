using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnapElectric : MonoBehaviour
{
    //private PlayerControls inputAction;
    private SnapController controller;
    
    public enum InputMapType
    {
        CoopClavier,
        ClavierManette,
        MultiManettes
    };
    
    
    private void OnEnable()
    {
       
        //inputAction.Player.Enable();
    }

    private void OnDisable()
    {
      
        //inputAction.Player.Disable();
    }
    
    public InputMapType InputMap;
    
    void Awake()
    {
        controller = GetComponent<SnapController>();
        //inputAction = new PlayerControls();

        switch (InputMap)
        {
            case InputMapType.CoopClavier :
                
                //inputAction.Player.Special.performed += ctx => Electric();
                //inputAction.Player.Special.canceled += ctx => StopElectric();
                break;
                
            case InputMapType.ClavierManette :
                break;
        }
    }

    private ElectricZone elecScript;
    
    void Start()
    {
        elecScript = elecZone.GetComponent<ElectricZone>();
    }

    public GameObject elecZone;

    private void Electric()
    {
        elecZone.SetActive(true);
        controller.canMove = false;
    }

    private void StopElectric()
    {
        elecScript.DisableElements();
        controller.canMove = true;
        elecZone.SetActive(false);
    }
    
}


