using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEditor.MPE;
using UnityEngine;

public class CrabeController : MonoBehaviourPunCallbacks, IPunObservable
{
    public static GameObject LocalPlayerInstance;
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    
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
        
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        if (!CameraManager.instance.isCinematique)
        {
            if (moveX > 0 && moveZ > 0)
            {
                rb.velocity += (move * speed/2 * Time.deltaTime);
            }
            else
            {
                rb.velocity += (move * speed * Time.deltaTime);
            }  
        }
    }
}
