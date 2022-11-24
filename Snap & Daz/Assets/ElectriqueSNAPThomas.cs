using UnityEngine;

public class ElectriqueSNAPThomas : MonoBehaviour
{
    public GameObject receiver; //Objet qui reçoit l'information électrique
    public ElectricElements receiverScript;

    public bool _canActive;
    public bool _isActive;
    [SerializeField] private GameObject _UIInteract;

    void Start()
    {
        receiverScript = receiver.GetComponent<ElectricElements>();
    }

    void Update()
    {
        _isActive = _canActive && Input.GetKey(KeyCode.G); //Activé seulement si "peut être activé et bouton F appuyé"

        receiverScript.isElectrified = _isActive;
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            _UIInteract.SetActive(true);
            _canActive = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            _UIInteract.SetActive(false);
            _canActive = false;
        }
    }
}
