using UnityEngine;

public class ElevatorBehaviourThomas : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;

    public bool isElectrified;
    public bool isActivated;

    public float speed;

    public Vector3 direction;

    public void MoveElevators() //Se lit quand un levier est activé pour déplacer les ascenceurs
    {
        isActivated = !isActivated;
    }

    public void Electrify() //Se lit quand un interrupteur électrique bloque les ascenceurs
    {
        isElectrified = !isElectrified;
    }

    void Start()
    {
        direction = (endPos - startPos).normalized;
        transform.position = startPos;
    }

    void Update()
    {
        if (isElectrified) // Est bloqué si est électrifié
        {
            transform.position = transform.position;
            return;
        } 

        if (isActivated) // Monte les ascenceurs
        {
            if (Vector3.Distance(transform.position, endPos) >= 0.01f)
            {
                transform.Translate(direction * (speed * Time.deltaTime));
            }
        }
        else // Descend les ascenceurs
        {
            if (Vector3.Distance(transform.position, startPos) >= 0.01f)
            {
                transform.Translate(-direction * (speed * Time.deltaTime));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer is 8 or 7)
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer is 8 or 7)
        {
            other.transform.parent = null;
        }
    }
}
