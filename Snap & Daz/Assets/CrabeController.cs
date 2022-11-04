using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CrabeController : MonoBehaviourPunCallbacks, IPunObservable
{
    public static GameObject LocalPlayerInstance;
    public static CrabeController instance;
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) //
    {
        if (stream.IsWriting)
        {
            
            // tous les trucs à envoyer
            
           /* stream.SendNext(IsFiring);
            stream.SendNext(health);*/
        }
        else
        {
            
            // tous les trucs à lire
            
           /* this.IsFiring = (bool)stream.ReceiveNext();
            this.health = (float)stream.ReceiveNext();*/
        }
    }


    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        
        //Vector3 move = transform.right * moveX + transform.forward * moveZ;
        Vector3 rotationMove = new Vector3(moveX, 0, moveZ);
        rotationMove.Normalize();

        if (!CameraManager.instance.isCinematique)
        {
           
            
            if (rotationMove != Vector3.zero)
            {
                rb.velocity += (rotationMove * speed * Time.deltaTime);
                Quaternion rotateTo = Quaternion.LookRotation(rotationMove,Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation,rotateTo,rotateSpeed * Time.deltaTime);
            }
            else
            {
                rb.velocity += (rotationMove * speed/2 * Time.deltaTime);
               
            }
        }
    }
}
