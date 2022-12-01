using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RequestOwnership : MonoBehaviour
{
    public void OnOwnershipRequest(object[] viewAndPlayer)
    {
        PhotonView view = viewAndPlayer[0] as PhotonView;
        Player requestingPlayer = viewAndPlayer[1] as Player;

        Debug.Log("OnOwnershipRequest(): Player " + requestingPlayer + "requests ownership of: " + view + ".");
        
        view.TransferOwnership(requestingPlayer.ActorNumber);
    }
}
