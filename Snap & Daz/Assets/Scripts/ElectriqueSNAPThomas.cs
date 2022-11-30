using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ElectriqueSNAPThomas : MonoBehaviour
{
    [SerializeField] private GameObject _UIInteract;
    public bool _canActive;

    public UnityEvent electricBehaviour;

    // public void PressF(InputAction.CallbackContext context)
    // {
    //     if (context.started && _canActive || context.canceled && _canActive)
    //     {
    //         Debug.Log("action du joueur avec l'interrupteur électrique");
    //         
    //         electricBehaviour.Invoke();
    //     }
    // }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _canActive)
        {
            electricBehaviour.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.F) && _canActive)
        {
            electricBehaviour.Invoke();
        }
    }
    
    public void OnTriggerEnter(Collider other) //Détecte SNAP quand il est proche
    {
        if (other.gameObject.layer == 7)
        {
            _UIInteract.SetActive(true);
            _canActive = true;
        }
    } 

    public void OnTriggerExit(Collider other) //Détecte SNAP quand il part
    {
        if (other.gameObject.layer == 7)
        {
            _UIInteract.SetActive(false);
            _canActive = false;
        }
    }
}
