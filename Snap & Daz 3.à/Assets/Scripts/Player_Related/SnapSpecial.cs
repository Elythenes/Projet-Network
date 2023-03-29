using UnityEngine;
using UnityEngine.InputSystem;

public class SnapSpecial : MonoBehaviour
{
    public GameObject elecZone;
    private ElectricZone elecScript;
    private ThomasCrabController controller;
    
    public void OnAbility(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Special();
        }

        if (ctx.canceled)
        {
            StopSpecial();
        }
    }
    
    void Start()
    {
        controller = GetComponent<ThomasCrabController>();
        elecScript = elecZone.GetComponent<ElectricZone>();
    }

    private void Special()
    {
        elecZone.SetActive(true);
        controller.canMove = false;
    }

    private void StopSpecial()
    {
        elecScript.DisableElements();
        controller.canMove = true;
        elecZone.SetActive(false);
    }
}
