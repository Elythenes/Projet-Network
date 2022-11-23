using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrabeController : MonoBehaviourPunCallbacks, IPunObservable
{
    public static GameObject LocalPlayerInstance;
    public static CrabeController instance;
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;

    private Vector3 rotationMove;
    public InputMaster playerInput;

    [Header("Variables pour SNAP")] 
    public bool isWalled;
    public float WallSpeed;
    public ConstantForce wallGravity;

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
        //Vector3 move;
        //move = playerInput.Player.Move.ReadValue<Vector2>();
        
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        //Vector3 move = transform.right * moveX + transform.forward * moveZ;
        if (!isWalled)
        {
            speed = 850;
            wallGravity.force = new Vector3(0,0,0);
           rotationMove = new Vector3(moveX, 0, moveZ);
           rotationMove.Normalize();
        }
        else
        {
            speed = WallSpeed;
            wallGravity.force = new Vector3(80,0,0);
            rotationMove = new Vector3(0, moveX,moveZ);
            rotationMove.Normalize();
        }
       
       

        if (!CameraManager.instance.isCinematique)
        {
            if (!isWalled)
            {
                if (rotationMove != Vector3.zero)
                {
                    rb.AddForce(rotationMove * speed * Time.deltaTime);
                    Quaternion rotateTo = Quaternion.LookRotation(rotationMove,Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation,rotateTo,rotateSpeed * Time.deltaTime);
                }
                else
                {
                    rb.velocity += (rotationMove * speed/2 * Time.deltaTime);
               
                }
            }
            else
            {
                if (rotationMove != Vector3.zero)
                {
                    rb.AddForce(rotationMove * speed * Time.deltaTime);
                    Quaternion rotateTo = Quaternion.LookRotation(rotationMove, Vector3.right);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation,rotateTo,rotateSpeed * Time.deltaTime);
                }
                else
                {
                    rb.velocity += (rotationMove * speed/2 * Time.deltaTime);
               
                }
            }
           
        }
    }
}
