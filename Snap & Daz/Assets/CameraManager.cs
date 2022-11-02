using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public float zoomSpeed;
    public Camera camera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] Transform player;
    [SerializeField] float maxDistance;
    public GameObject ping;
    public bool isCinematique;
    public bool isMoving;
    private float timeCiné;
    private float timeCinéTimer;
    public float preferredDistance;
    private Vector3 PlayerPos;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PlayerPos = transform.position;
    }

    private void Update()
    {
        if (!isCinematique)
        {
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
        isCinematique = true;
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
}
