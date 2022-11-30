using UnityEngine;
using DG.Tweening;

public class DoorBehaviour : MonoBehaviour
{
    public Vector3 positionDifferenceWhenOpen;
    public float openingDuration;
    public float closingDuration;
    public float timeMoveCamera;
    private Vector3 positionWhenClosed;
    public bool cinematiquePorte = false;
    public int numberOfKey = 1;
    private int keyActivated;

    private void Start()
    {
        positionWhenClosed = gameObject.transform.position;
        keyActivated = 0;
    }

    public void Key()
    {
        keyActivated += 1;
        if (keyActivated >= numberOfKey)
        {
            transform.DOMove(transform.position + positionDifferenceWhenOpen, openingDuration);
            if (cinematiquePorte)
            {
                CameraManager.instance.CinematiquePorte(gameObject,timeMoveCamera); 
            }
        }
    }

    public void FermeturePorte()
    {
        keyActivated -= 1;
        transform.DOMove(positionWhenClosed, closingDuration);
    }
}
