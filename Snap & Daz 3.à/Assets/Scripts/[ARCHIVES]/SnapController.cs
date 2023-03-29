using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnapController : MonoBehaviour
{
    
    #region Trucs Chelou pour les Input
   // private PlayerControls inputAction;
    private void OnEnable()
    {
       
        //inputAction.Player.Enable();
    }

    private void OnDisable()
    {
      
        //inputAction.Player.Disable();
    }

    #endregion
    
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    private float originalSpeed;
    [SerializeField] float rotateSpeed;
    public bool canRotate = true;
    private bool isDiagonal;
    public LayerMask ground;
  
    
   
    
    #region Interaction avec les objets

    public GameObject interactibleObject;
    
    #endregion

    void Awake()
    {
        //inputAction = new PlayerControls();
        
        //inputAction.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector3>();
        //inputAction.Player.Interact.performed += ctx => Interact();
        //inputAction.Player.Interact.canceled += ctx => StopInteract();
    }

    public float gravityScale;
    public float globalGravity;
    public Vector3 moveInput;


    [Header("Variables pour SNAP")]
    public float wallOrientation; // Orientation du mur dans le sens des aiguilles d'un montre
    public bool canMove;
    public float WallSpeed;
    public ConstantForce wallGravity;
    
    private bool _isInteracting;
    
    private void Start()                                                
    {
        originalSpeed = speed;
    }                                                                   

 

    private void Update()
    {

        //Vector3 move;
        //move = playerInput.Player.Move.ReadValue<Vector2>();


        if (!_isInteracting) // Empêche le déplacement si le joueur intéragit avec un élément
        {
            if (moveInput.x != 0 && moveInput.z != 0)
            {
                isDiagonal = true;
            }
            else
            {
                isDiagonal = false;
            }

            Debug.DrawRay(transform.position, transform.forward - transform.up, Color.green);

            if (!Physics.Raycast(transform.position, transform.forward - transform.up, ground))
            {
                moveInput.z = Mathf.Clamp(moveInput.y, -99, 0);
                rb.velocity = Vector3.zero;
            }

            speed = originalSpeed;
            wallGravity.force = new Vector3(0, 0, 0);
        }
        
        if (moveInput != Vector3.zero && canMove)
        {
                rb.velocity += (moveInput * (speed * Time.deltaTime));
                if (canRotate)
                {
                    Quaternion rotateTo = Quaternion.LookRotation(moveInput, Vector3.up);
                    transform.rotation =
                        Quaternion.RotateTowards(transform.rotation, rotateTo, rotateSpeed * Time.deltaTime);
                }

        }
        else if (moveInput.x != 0 && moveInput.y != 0 && canMove)
        {
            rb.velocity += (moveInput * speed / 2 * Time.deltaTime);
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
