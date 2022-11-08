using UnityEngine;
using System.Collections.Generic;
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
    public TextMeshProUGUI nickName;

    public List<PlayerItem> playerItemsList = new List<PlayerItem>();
    public PlayerItem playerItemPrefab;
    public Transform playerItemParent;

    public GameObject playButton;
    
    private PlayerItem newPlayerItem;
    private bool isConnected;

    private void Start()
    {
        PhotonNetwork.JoinLobby();
        nickName.text = PlayerPrefs.GetString("PlayerName");
    }

    void Update()
    {
        createButton.interactable = createRoomInputField.text.Length >= 1;
        joinButton.interactable = joinRoomInputField.text.Length >= 1;

        if (isConnected && PhotonNetwork.IsMasterClient)
        {
            playButton.SetActive(true);
        }
        else
        {
            playButton.SetActive(false);
        }
        
        if (isConnected && PhotonNetwork.CurrentRoom.PlayerCount >= 2 && PhotonNetwork.IsMasterClient)
        {
            playButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            playButton.GetComponent<Button>().interactable = false;
        }
    }

    public void OnClickCreate()
    {
        if (createRoomInputField.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(createRoomInputField.text, new RoomOptions() { MaxPlayers = 2, BroadcastPropsChangeToAll = true});
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
        isConnected = true;
        UpdatePlayerList();
    }

    public void OnClickLeaveRoom()
    {
        isConnected = false;
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);

        createRoomInputField.text = "";
        joinRoomInputField.text = "";
    }

    void UpdatePlayerList()
    {
        foreach (PlayerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
        }
        playerItemsList.Clear();

        if (PhotonNetwork.CurrentRoom == null) return;

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
           newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
           newPlayerItem.SetPlayerInfo(player.Value);

           if (Equals(player.Value, PhotonNetwork.LocalPlayer))
           {
               newPlayerItem.ApplyLocalChanges();
           }
           
           playerItemsList.Add(newPlayerItem);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }
    
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    public void OnClickPlayButton()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
