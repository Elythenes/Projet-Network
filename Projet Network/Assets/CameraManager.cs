using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Photon.Pun;
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
    private float timeCiné;
    private float timeCinéTimer;

    private void Awake()
    {
        instance = this;
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
                 /* Vector3 mousePos = raycastHit.point;        // Petit mouvement de la camera vers la souris autour du personnage
 
            Vector3 difference = mousePos - player.position;
            float magnitude = difference.magnitude;
            if (magnitude > maxDistance) 
            {
                difference = difference * (maxDistance / magnitude);
            }
            transform.position = new Vector3(player.position.x + difference.x,transform.position.y,player.position.z + difference.z);     */

                 if (Input.GetKeyDown(KeyCode.Mouse0))
                 {
                
                     //PhotonNetwork.Instantiate("ping", raycastHit.point, Quaternion.identity, 1);   // Ping
                     GameObject pingObj = Instantiate(ping, raycastHit.point, Quaternion.Euler(-90,0,0));
                     Destroy(pingObj,1f);
                 }
             }
         }
    }

    public void CinematiquePorte(GameObject porte, float timeToGo)
    {
       
        isCinematique = true;
        transform.DOMove(porte.transform.position,timeToGo);
        
    }
    
}
