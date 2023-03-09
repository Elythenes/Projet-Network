using System.Collections;
using UnityEngine;

public class Door : Activable
{
    [Header("Positions")] 
    public Vector3 startingPos;
    public Vector3 endingPos;

    [Header("Speed")] 
    public float travelTime;

    public void Start()
    {
        startingPos = transform.position;
    }

    public override void Activate()
    {
        base.Activate();

        Debug.Log("Activation");

        StartCoroutine(Move(startingPos, endingPos, travelTime));
    }

    public override void Desactivate()
    {
        base.Desactivate();

        Debug.Log("Desactivation");

        StartCoroutine(Move(endingPos, startingPos, travelTime));
    }

    private IEnumerator Move(Vector3 a, Vector3 b, float time)
    {
        for (float t = 0; t < 1; t += Time.deltaTime / time)
        {
            transform.position = Vector3.Lerp(a, b, t);
            yield return null;
        }
    }
}
