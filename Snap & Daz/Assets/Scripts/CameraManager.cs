using DG.Tweening;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public Camera camera;
    
    [Header("Données à trouver")]
    [SerializeField] private LayerMask layerMask;
    public Transform player;
    public GameObject ping;
    
    [Header("Données à changer")]
    public float zoomSpeed;
    public float preferredDistance;
    [Range(0.01f, 1.0f)] public float SmoothFactor = 0.5f;

    [Header("Données passives / test")] 
    public Vector3 cameraOffset;
    public bool isMoving;
    public bool isCinematique;
    
    private Vector3 PlayerPos;
    private float timeCinéTimer;
    private float timeCiné;

    private void Start()
    {
        PlayerPos = transform.position;
        //cameraOffset = transform.position - player.transform.position;
    }

    private void FixedUpdate()
    {
        
        if (!isCinematique)
        {
            PlayerPos = transform.position;
            Vector3 newPos = player.transform.position + cameraOffset;
            transform.position = Vector3.Lerp(transform.position,newPos,SmoothFactor);
            
            if (camera.fieldOfView > 60)
            {
                camera.fieldOfView = 60;
            }
            else
            {
                camera.fieldOfView -= Input.mouseScrollDelta.y * zoomSpeed;
            }

            if (camera.fieldOfView <= 25)
            {
                camera.fieldOfView = 25;
            }
            else
            {
                camera.fieldOfView += Input.mouseScrollDelta.x * zoomSpeed;
            }
        }


        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, layerMask))
        {
            if (!isCinematique)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {

                    //PhotonNetwork.Instantiate("ping", raycastHit.point, Quaternion.identity, 1);   // Ping
                    GameObject pingObj = Instantiate(ping, raycastHit.point, Quaternion.Euler(-90, 0, 0));
                    Destroy(pingObj, 1f);
                }
            }
        }
    }

    public void CinematiquePorte(GameObject porte, float timeToGo)
    {
        Vector3 originalPos = transform.position;
        float originalFoV = camera.fieldOfView;
        isCinematique = true;
        camera.fieldOfView = 25;
        isMoving = true;

        //transform.DOMove(porte.transform.position - new Vector3(5,-5,-5),timeToGo);
        if (isMoving)
        {
            Vector3 distanceVector = transform.position - porte.transform.position;
            Vector3 distanceVectorNormalized = distanceVector.normalized;
            Vector3 targetPosition = porte.transform.position + (distanceVectorNormalized * preferredDistance);

            //transform.position = Vector3.Lerp(transform.position, targetPosition, timeToGo);
            transform.LookAt(targetPosition);
            transform.DOMove(targetPosition, timeToGo).OnComplete
                (() => RetourPerso());
            camera.fieldOfView = originalFoV;

            //var offsetX = Math.Abs(transform.position.x - targetPosition.x);
            //var offsetZ = Math.Abs(transform.position.z - targetPosition.z);
        }
    }


    public void RetourPerso()
    {
        isMoving = false;
        isCinematique = false;
        
        transform.position = PlayerPos;
        transform.LookAt(player.transform);
    }

    public void SetTarget(Transform target)
    {
        this.player = target;
    }
}
