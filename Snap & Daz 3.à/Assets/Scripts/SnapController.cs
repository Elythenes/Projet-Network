using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnapController : MonoBehaviour
{
    
    #region Trucs Chelou pour les Input
    private PlayerControls inputAction;
    private void OnEnable()
    {
       
        inputAction.Player.Enable();
    }

    private void OnDisable()
    {
      
        inputAction.Player.Disable();
    }

    #endregion
    
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    private float originalSpeed;
    [SerializeField] float rotateSpeed;
    public bool canRotate = true;
    private bool isDiagonal;
    public LayerMask ground;
    public enum InputMapType
    {
        CoopClavier,
        ClavierManette,
        MultiManettes
    };
    
    public enum Player
    {
        P1,
        P2
    };
    
    #region Interaction avec les objets

    public GameObject interactibleObject;
    
    #endregion

    void Awake()
    {
        inputAction = new PlayerControls();

        switch (InputMap)
             {
                case InputMapType.CoopClavier :
               
                    inputAction.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector3>();
                    inputAction.Player.Interact.performed += ctx => Interact();
                    inputAction.Player.Interact.canceled += ctx => StopInteract();
                    break;
                
                case InputMapType.ClavierManette :
                    break;
             }
       
    }

    public Player playerNumber;
    public InputMapType InputMap;
    
    public float gravityScale;
    public float globalGravity;
    [HideInInspector] public Vector3 rotationVector;
    [HideInInspector] public Vector3 moveVector;
    public Vector3 moveInput;


    [Header("Variables pour SNAP")]
    public float wallOrientation; // Orientation du mur dans le sens des aiguilles d'un montre
    public bool isWalled;
    public float WallSpeed;
    public ConstantForce wallGravity;
    
    private bool _isInteracting;
    
    private void Start()                                                
    {

        originalSpeed = speed;        
        
        
    }                                                                   

   /* private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * (gravityScale * globalGravity),ForceMode.Acceleration);
    }*/
    
    private void Update()
    {
    
        //Vector3 move;
        //move = playerInput.Player.Move.ReadValue<Vector2>();
        

        if (!_isInteracting)  // Empêche le déplacement si le joueur intéragit avec un élément
        {
            if (moveInput.x != 0 && moveInput.z != 0)
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
                moveInput.z = Mathf.Clamp(moveInput.y, -99, 0);
                rb.velocity = Vector3.zero;
            }
        
            if (!isWalled)
            {
                speed = originalSpeed;
                wallGravity.force = new Vector3(0,0,0);
                rotationVector = new Vector3( moveInput.x, 0,  moveInput.y);
                moveVector = new Vector3(-moveInput.x, 0,  moveInput.y);
                rotationVector.Normalize();
            }
            else
            {
                switch (wallOrientation)
                {
                    case 1:
                        wallGravity.force = new Vector3(0,0,-80);
                        speed = WallSpeed;
                        rotationVector = new Vector3(- moveInput.x,  moveInput.y,0);
                        moveVector =  new Vector3( moveInput.x, - moveInput.y,0);
                        rotationVector.Normalize();
                        break;
                    case 4:
                        wallGravity.force = new Vector3(80,0,0);
                        speed = WallSpeed;
                        rotationVector = new Vector3(0, - moveInput.x,- moveInput.y);
                        moveVector =  new Vector3(0,  moveInput.x, moveInput.y);
                        rotationVector.Normalize();
                        break;
                }
            }
        }
        
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
                        rb.velocity += (rotationVector * (WallSpeed/1.5f * Time.deltaTime));    
                    }
                    else
                    {
                        rb.velocity += (rotationVector * (WallSpeed * Time.deltaTime));    
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
    
    public void Interact()
    {
        if (interactibleObject == null) return;

        var interactible = interactibleObject.GetComponent<Interactible>();

        interactible.actor = gameObject;
        interactible.Interact();
    }

    public void StopInteract()
    {
        if (interactibleObject == null) return;

        var interactible = interactibleObject.GetComponent<Interactible>();

        interactible.actor = null;
        interactible.StopInteract();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactible"))
        {
            interactibleObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactible"))
        {
            interactibleObject = null;
        }
    }
}
