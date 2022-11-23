using UnityEngine;
using Photon.Pun;

public class PlayerControllerTest : MonoBehaviour, IPunObservable
{
    public Rigidbody rb;
    public PhotonView photonView;
    
    private float _tempX;
    private float _tempY;

    void Update()
    {
        if (!photonView.IsMine) return;
        
        Debug.Log("isMine");
        
        if (Input.GetKey(KeyCode.W))
        {
            _tempY = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _tempY = -1;
        }
        else
        {
            _tempY = 0;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _tempX = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _tempX = 1;
        }
        else
        {
            _tempX = 0;
        }

        rb.velocity = new Vector3(_tempX, 0, _tempY) * 10f;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(rb.position);
            stream.SendNext(rb.rotation);
            stream.SendNext(rb.velocity);
        }
        else
        {
            rb.position = (Vector3)stream.ReceiveNext();
            rb.rotation = (Quaternion)stream.ReceiveNext();
            rb.velocity = (Vector3)stream.ReceiveNext();
            
            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.timestamp));
            rb.position += rb.velocity * lag;
        }
    }
}
