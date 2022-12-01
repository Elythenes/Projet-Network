using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class LevierDAZThomas : MonoBehaviour
{
    [SerializeField] private GameObject _UIInteract;
    public bool _canActive;

    public UnityEvent leverBehaviour;

    // public void PressF(InputAction.CallbackContext context)
    // {
    //     if (context.started && _canActive || context.canceled && _canActive)
    //     {
    //         Debug.Log("action du joueur avec le levier");
    //         
    //         leverBehaviour.Invoke();
    //     }
    // }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _canActive)
        {
            leverBehaviour.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.F) && _canActive)
        {
            leverBehaviour.Invoke();
        }
    }

    public void OnTriggerEnter(Collider other) //Détecte DAZ quand il est proche
    {
        if (other.gameObject.layer == 8)
        {
            _UIInteract.SetActive(true);
            _canActive = true;
        }
    } 

    public void OnTriggerExit(Collider other) //Détecte DAZ quand il part
    {
        if (other.gameObject.layer == 8)
        {
            _UIInteract.SetActive(false);
            _canActive = false;
        }
    } 
}
