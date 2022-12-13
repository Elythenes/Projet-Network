using Photon.Pun;
using UnityEngine;

public class SnapController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    private float originalSpeed;
    [SerializeField] float rotateSpeed;
    public bool canRotate = true;
    public bool isDiagonal;
    public LayerMask ground;

    [HideInInspector] public Vector3 rotationVector;
    [HideInInspector] public Vector3 moveVector;
    
    
    [Header("Variables pour SNAP")]
    public float wallOrientation; // Orientation du mur dans le sens des aiguilles d'un montre
    public bool isWalled;
    public float WallSpeed;
    public ConstantForce wallGravity;
    public PhotonView photonView;
    
    public ActivatedElements activatedElements;

    private bool _isInteracting;
    
    private void Start()                                                
    {                                                                   
        if (!photonView.IsMine) return;                                 
                                                                        
        originalSpeed = speed;                                          
    }                                                                   

    private void Update()
    {
        if (!photonView.IsMine) return;
                               
        //Vector3 move;
        //move = playerInput.Player.Move.ReadValue<Vector2>();

        _isInteracting = activatedElements is not null && Input.GetKey(KeyCode.F);

        if (!_isInteracting)  // Empêche le déplacement si le joueur intéragit avec un élément
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveZ = Input.GetAxisRaw("Vertical");
            if (moveX != 0 && moveZ != 0)
        {
            isDiagonal = true;
        }
            else
        {
            isDiagonal = false;
        }
            Debug.DrawRay(transform.position,transform.forward - transform.up,Color.green);
            if (!Physics.Raycast(transform.position, transform.forward - transform.up,ground))
            {
                moveZ = Mathf.Clamp(moveZ, -99, 0);
                rb.velocity = Vector3.zero;
            }
        
            if (!isWalled)
        {
            speed = originalSpeed;
            wallGravity.force = new Vector3(0,0,0);
           rotationVector = new Vector3(moveX, 0, moveZ);
           moveVector = new Vector3(moveX, 0, moveZ);
           rotationVector.Normalize();
        }
            else
            {
                switch (wallOrientation)
                {
                    case 1:
                        wallGravity.force = new Vector3(0,0,-80);
                        speed = WallSpeed;
                        rotationVector = new Vector3(-moveX, moveZ,0);
                        moveVector =  new Vector3(moveX, -moveZ,0);
                        rotationVector.Normalize();
                        break;
                    case 4:
                        wallGravity.force = new Vector3(80,0,0);
                        speed = WallSpeed;
                        rotationVector = new Vector3(0, -moveX,-moveZ);
                        moveVector =  new Vector3(0, moveX,moveZ);
                        rotationVector.Normalize();
                        break;
                }
            }
        }
        
        if (CameraManager.instance is null || !CameraManager.instance.isCinematique)
        {
            if (!isWalled)
            {
                if (rotationVector != Vector3.zero)
                {
                    rb.velocity += (rotationVector * (speed * Time.deltaTime));
                    if (canRotate)
                    {
                        Quaternion rotateTo = Quaternion.LookRotation(rotationVector,Vector3.up);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation,rotateTo,rotateSpeed * Time.deltaTime);
                    }
                
                }
                else if(rotationVector.x != 0 && rotationVector.y != 0)
                {
                    rb.velocity += (rotationVector * speed/2 * Time.deltaTime);
                }
            }
            else
            {
                if (rotationVector != Vector3.zero)
                {
                    if (isDiagonal)
                    {
                        rb.velocity += (moveVector * (WallSpeed/1.5f * Time.deltaTime));    
                    }
                    else
                    {
                        rb.velocity += (moveVector * (WallSpeed * Time.deltaTime));    
                    }
                    
                    if (canRotate)
                    {
                        if (wallOrientation == 1)
                        {
                            Quaternion rotateTo = Quaternion.LookRotation(-rotationVector , Vector3.forward);
                            transform.rotation = Quaternion.RotateTowards(transform.rotation,rotateTo,rotateSpeed * Time.deltaTime); 
                        }
                      
                        else if (wallOrientation == 4)
                        {
                            Quaternion rotateTo = Quaternion.LookRotation(-rotationVector, Vector3.left);
                            transform.rotation = Quaternion.RotateTowards(transform.rotation,rotateTo,rotateSpeed * Time.deltaTime); 
                        }
                       
                      
                    }
                }
            }
           
        }

        if ((Input.GetKeyDown(KeyCode.F) || Input.GetKeyUp(KeyCode.F)) && activatedElements is not null)
        {
            activatedElements.SwitchActivation();
        }
    }
}
