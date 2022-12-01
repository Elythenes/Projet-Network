using UnityEngine;
using UnityEngine.Events;

public class LevierDAZThomas : ActivatedElements
{
    [SerializeField] private GameObject _UIInteract;

    public UnityEvent leverBehaviour;

    public ActivatedElements activatedElements;

    void Start()
    {
        activatedElements = GetComponent<ActivatedElements>();
    }

    void Update()
    {
        if (isActivated)
        {
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

            other.GetComponent<DazController>().activatedElements = null;
        }
    } 
}
