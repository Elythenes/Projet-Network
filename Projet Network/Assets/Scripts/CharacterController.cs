using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed;
    public float forceJump;
    public Rigidbody rb;
    public LayerMask groundLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        
        rb.AddForce(new Vector3(moveX * speed,0,moveZ * speed));

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, 5,groundLayer))
        {
            Debug.DrawRay(transform.position, Vector3.down, Color.green);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.down, Color.red);
        }
      
    }

    void Jump()
    {
        rb.AddForce(new Vector3(0,forceJump,0));
    }
}
