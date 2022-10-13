using Photon.Pun;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviourPun
{
    private Animator animator;
    
    [SerializeField] private float directionDampTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (!animator)
        {
            Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
        }
    }

    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        
        if (!animator)
        {
            return;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Base Layer.Run"))
        {
            if (Input.GetButtonDown("Fire2"))
            {
                animator.SetTrigger("Jump");
            }
        }

        if (stateInfo.IsName("Base Layer.Idle 0"))
        {
            if (z == 0)
            {
                Vector3 rota = transform.rotation.eulerAngles;
                rota = rota.y * new Vector3(0,z,0);
                transform.rotation = Quaternion.Euler(rota);
            }
        }
        
        
        if(z >= 0)
        {
            transform.Translate(new Vector3(x * 0.01f,0,z * 0.01f));
        }
            
        if(stateInfo.IsName("Base Layer.Run O"))
        {
            Debug.Log(z);
            transform.Translate(new Vector3(0,0,10));
        }
        

        

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down * 5))
        {
            Debug.DrawRay(transform.position, Vector3.down * 5, Color.green);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.down, Color.red);
        }
      
        animator.SetFloat("Speed", z);
        animator.SetFloat("Direction", x, directionDampTime, Time.deltaTime);
    }

    void Jump()
    {
        transform.Translate((new Vector3(0,10,0)));
    }
}
