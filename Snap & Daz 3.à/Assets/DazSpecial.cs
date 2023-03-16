using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DazSpecial : MonoBehaviour
{
    private ThomasCrabController controller;

    public GameObject destructibleObject;
    
    public void OnAbility(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            StartCoroutine(Special());
        }
    }
    
    void Start()
    {
        controller = GetComponent<ThomasCrabController>();
    }

    private IEnumerator Special()
    {
        controller.canMove = false;

        if (destructibleObject != null)
        {
            Destroy(destructibleObject);
        }

        yield return new WaitForSeconds(0.25f);
        
        controller.canMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destructible"))
        {
            destructibleObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Destructible"))
        {
            destructibleObject = null;
        }
    }
}
