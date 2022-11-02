using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField createRoomInputField;
    public TMP_InputField joinRoomInputField;

    public Button createButton;
    public Button joinButton;
    
    public GameObject lobbyPanel;
    public GameObject roomPanel;
    
    public TextMeshProUGUI roomName;

    private void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    void Update()
    {
        createButton.interactable = createRoomInputField.text.Length >= 1;
        joinButton.interactable = joinRoomInputField.text.Length >= 1;
    }

    public void OnClickCreate()
    {
        if (createRoomInputField.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(createRoomInputField.text, new RoomOptions() { MaxPlayers = 2 });
        }
    }
    
    public void OnClickJoin()
    {
        if (joinRoomInputField.text.Length >= 1)
        {
            PhotonNetwork.JoinRoom(joinRoomInputField.text);
        }
    }

    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = "Room Name : " + PhotonNetwork.CurrentRoom.Name;
    }
}
