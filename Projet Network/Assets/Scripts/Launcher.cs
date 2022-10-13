using Photon.Realtime;
using Photon.Pun;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";

    [Tooltip("The maximum number of players per room.")]
    [SerializeField] 
    private byte maxPlayersPerRoom = 4;

    [Tooltip("The UI Panel to let the user enter name, connect and play")]
    [SerializeField] private GameObject controlPanel;
    
    [Tooltip("The UI Panel to inform the user that the connection is in progress")]
    [SerializeField] private GameObject progressLabel;

    private bool isConnecting;
    
    
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        controlPanel.SetActive(true);
        progressLabel.SetActive(false);
    }

    public void Connect()
    {
        controlPanel.SetActive(false);
        progressLabel.SetActive(true);
        
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinRandomFailed() was called by PUN. No random room available, so we create one. \nCalling: PhotonNetwork.CreateRoom");
        PhotonNetwork.CreateRoom(null, new RoomOptions{MaxPlayers = maxPlayersPerRoom});
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("We load the 'Room for 1' ");

            PhotonNetwork.LoadLevel("Room for 1");
        }
    }
    
    #region MonoBehaviourPunCallbacks Callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
        
        if(isConnecting)
        { 
            PhotonNetwork.JoinRandomRoom();
            isConnecting = false;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        controlPanel.SetActive(true);
        progressLabel.SetActive(false);
        isConnecting = false;
        
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }
    
    #endregion
}
