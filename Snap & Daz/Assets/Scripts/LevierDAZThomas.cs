using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class LevierDAZThomas : ActivatedElements
{
    [SerializeField] private GameObject _UIInteract;

    public UnityEvent leverBehaviour;

    private ActivatedElements activatedElements;

    public PhotonView[] photonViews;

    public bool isElectrified;
    
    private void Start()
    {
        activatedElements = GetComponent<ActivatedElements>();
    }
    
    public void Electrify() //Se lit quand un interrupteur électrique bloque les ascenceurs
    {
        isElectrified = !isElectrified;
    }

    void Update()
    {
        if (isActivated)
        {
            if (!isElectrified)
            {
                foreach (var photonView in photonViews)
                {
                    photonView.RequestOwnership();
                }
            }
            
            leverBehaviour.Invoke();
            isActivated = false;
        }
    }

    public void OnTriggerEnter(Collider other) //Détecte DAZ quand il est proche
    {
        if (other.gameObject.layer == 8)
        {
            _UIInteract.SetActive(true);

            other.GetComponent<DazController>().activatedElements = activatedElements;
        }
    } 

    public void OnTriggerExit(Collider other) //Détecte DAZ quand il part
    {
        if (other.gameObject.layer == 8)
        {
            _UIInteract.SetActive(false);

            isActivated = false;

            other.GetComponent<DazController>().activatedElements = null;
        }
    } 
}
