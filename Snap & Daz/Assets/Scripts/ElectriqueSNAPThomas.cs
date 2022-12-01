using UnityEngine;
using UnityEngine.Events;

public class ElectriqueSNAPThomas : ActivatedElements
{
    [SerializeField] private GameObject _UIInteract;

    public UnityEvent electricBehaviour;
    
    public ActivatedElements activatedElements;

    void Start()
    {
        activatedElements = GetComponent<ActivatedElements>();
    }

    void Update()
    {
        if (isActivated)
        {
            electricBehaviour.Invoke();
            isActivated = false;
        }
    }

    public void Activate()
    {
        electricBehaviour.Invoke();
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
