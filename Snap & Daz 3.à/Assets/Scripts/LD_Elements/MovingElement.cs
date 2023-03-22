using System.Collections;
using UnityEngine;

public class MovingElement : Activable
{
    [Header("Positions")] 
    private Vector3 startingPos;
    [Tooltip("Détermine la position d'arrivée de la plateforme. Celle de départ est la position actuelle")] public Vector3 endingPos;

    [Header("Speed")] 
    [Tooltip("Détermine le temps que met la plateforme pour atteindre sa position finale")] public float travelTime;

    private Vector3 currentPos;

    private Coroutine co;
    [HideInInspector] public bool cantMove;

    private float reTravelTime;

    public void Start()
    {
        startingPos = transform.position;
    }

    public override void Activate()
    {
        base.Activate();

        StopCo();

        if (isElectrified) return;

        reTravelTime = Vector3.Distance(endingPos, currentPos) / Vector3.Distance(endingPos, startingPos) * travelTime;
        
        co = StartCoroutine(Move(currentPos, endingPos, reTravelTime));
    }

    public override void Desactivate()
    {
        base.Desactivate();

        StopCo();
        
        if (isElectrified) return;
        if (cantMove) return;
        
        reTravelTime = Vector3.Distance(startingPos, currentPos) / Vector3.Distance(endingPos, startingPos) * travelTime;
        
        co = StartCoroutine(Move(currentPos, startingPos, reTravelTime));
    }

    private IEnumerator Move(Vector3 a, Vector3 b, float time)
    {
        for (float t = 0; t < 1; t += Time.deltaTime / time)
        {
            transform.position = Vector3.Lerp(a, b, t);
            yield return null;
        }
    }

    void Update()
    {
        currentPos = transform.position;
    }

    public override void Electrify()
    {
        base.Electrify();
        
        StopCo();
    }

    public override void DesElectrify()
    {
        base.DesElectrify();

        if (isActivated)
        {
            Activate();
        }
        else
        {
            Desactivate();
        }
    }

    public void StopCo()
    {
        if (co != null)
        {
            StopCoroutine(co);
        }
    }
}
