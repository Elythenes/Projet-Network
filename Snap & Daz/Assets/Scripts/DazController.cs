
using Photon.Pun;
using UnityEngine;

public class DazController : MonoBehaviour
{
     [SerializeField] Rigidbody rb;
    public PhotonView photonView;
    [HideInInspector] public Vector3 moveVector;
    [HideInInspector] public Vector3 rotationVector;
    public Vector3 oui;
    public LayerMask ground;
    [SerializeField] float rotateSpeed;
    [SerializeField] float speed;
    private float originalSpeed;
    public bool canRotate = true;

    public ActivatedElements activatedElements;
    
    private bool _isInteracting;

    private void Update()
    {
        if (!photonView.IsMine) return;
        
        _isInteracting = activatedElements is not null && Input.GetKey(KeyCode.F);
        
        // Empêche le déplacement si le joueur intéragit avec un élément
        if (!_isInteracting) 
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveZ = Input.GetAxisRaw("Vertical");
            Debug.DrawRay(transform.position,transform.forward - transform.up,Color.green);
            if (!Physics.Raycast(transform.position, transform.forward - transform.up,ground))
            {
                moveZ = Mathf.Clamp(moveZ, -99, 0);
                rb.velocity = Vector3.zero;
            }
            
            rotationVector = new Vector3(moveX, 0, moveZ);
            moveVector = new Vector3(moveX, 0, moveZ);
            rotationVector.Normalize();
        } 
        
        if (CameraManager.instance is null || !CameraManager.instance.isCinematique)
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
            else
            {
                rb.velocity += (rotationVector * speed/2 * Time.deltaTime);
               
            }
        }
        
        if ((Input.GetKeyDown(KeyCode.F) || Input.GetKeyUp(KeyCode.F)) && activatedElements is not null)
        {
            activatedElements.SwitchActivation();
        }
    }
}
