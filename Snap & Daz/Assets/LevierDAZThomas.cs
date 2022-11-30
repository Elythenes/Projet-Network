using UnityEngine;
using DG.Tweening;

public class LevierDAZThomas : ElectricElements
{
    public GameObject elevator1;
    public GameObject elevator2;
    public float timeBetweenFloors;

    private ElevatorBehaviourThomas _elevatorBehaviour1;
    private ElevatorBehaviourThomas _elevatorBehaviour2;

    [SerializeField] private GameObject _UIInteract;
    public bool _canActive;
    public bool _isActive;

    void Start()
    {
        _elevatorBehaviour1 = elevator1.GetComponent<ElevatorBehaviourThomas>();
        _elevatorBehaviour2 = elevator2.GetComponent<ElevatorBehaviourThomas>();
    }

    private void Update()
    {
        _isActive = _canActive && Input.GetKey(KeyCode.F); //Activé seulement si "peut être activé et bouton F appuyé"

        if (isElectrified)
        {
            elevator1.transform.position = elevator1.transform.position;
            elevator2.transform.position = elevator2.transform.position;
            return;
        }

        var direction1 = (_elevatorBehaviour1.endPos - _elevatorBehaviour1.startPos)/100;
        var direction2 = (_elevatorBehaviour2.endPos - _elevatorBehaviour2.startPos)/100;

        if (_isActive && Vector3.Distance(elevator1.transform.position, _elevatorBehaviour1.endPos) >= 0.2f)
        {
            elevator1.transform.Translate(direction1);
            elevator2.transform.Translate(direction2);
        }
        else if (_isActive && Vector3.Distance(elevator1.transform.position, _elevatorBehaviour1.endPos) <= 0.2f)
        {
            elevator1.transform.position = _elevatorBehaviour1.endPos;
            elevator2.transform.position = _elevatorBehaviour2.endPos;
        }
        else if (Vector3.Distance(elevator1.transform.position, _elevatorBehaviour1.startPos) <= 0.2f)
        {
            elevator1.transform.position = _elevatorBehaviour1.startPos;
            elevator2.transform.position = _elevatorBehaviour2.startPos;
        }
        else if (!_isActive)
        {
            elevator1.transform.Translate(-direction1);
            elevator2.transform.Translate(-direction2);
        }
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            _UIInteract.SetActive(true);
            _canActive = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            _UIInteract.SetActive(false);
            _canActive = false;
        }
    }
}
