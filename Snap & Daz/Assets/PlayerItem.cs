using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI playerName;

    public Image backgroundImage;
    public Color highlightColor;
    public GameObject leftArrowButton;
    public GameObject rightArrowButton;
    public GameObject readyBox;

    private ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    public Image playerAvatar;
    public Sprite[] avatars;

    private Player player;

    public void SetPlayerInfo(Player _player)
    {
        playerName.text = _player.NickName;
        player = _player;
        UpdatePlayerItem(player);
    }

    public void ApplyLocalChanges()
    {
        backgroundImage.color = highlightColor;
        leftArrowButton.SetActive(true);
        rightArrowButton.SetActive(true);
        readyBox.SetActive(true);
    }

    public void OnClickLeftArrow()
    {
        if ((int)playerProperties["playerAvatar"] == 0)
        {
            playerProperties["playerAvatar"] = avatars.Length - 1;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] - 1;
        }
    
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }
    
    public void OnClickRightArrow()
    {
        if ((int)playerProperties["playerAvatar"] == avatars.Length - 1)
        {
            playerProperties["playerAvatar"] = 0;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] + 1;
        }
        
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }
    
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable playerProperties)
    {
        if (player == targetPlayer)
        {
            UpdatePlayerItem(targetPlayer);
        }
    }
    
    void UpdatePlayerItem(Player player)
    {
        if (player.CustomProperties.ContainsKey("playerAvatar"))
        {
            playerAvatar.sprite = avatars[(int)player.CustomProperties["playerAvatar"]];
            playerProperties["playerAvatar"] = (int)player.CustomProperties["playerAvatar"];
        }
        else
        {
            playerProperties["playerAvatar"] = 0;
        }

        Image readyBoxColor = readyBox.GetComponent<Image>();
        
        if(player.CustomProperties.ContainsKey("playerReady"))
        {
            if ((int)player.CustomProperties["playerReady"] == 1)
            {
                readyBoxColor.color = new Color(255f, 131f, 131f);
                playerProperties["playerReady"] = 1;
            }
                
            else if ((int)player.CustomProperties["playerReady"] == 0)
            {
                readyBoxColor.color = new Color(209f, 180f, 157f);
                playerProperties["playerReady"] = 0;
            }
        }
        else
        {
            playerProperties["playerReady"] = 0;
        }
    }

    private int isReady;
    
    public void OnCheckReadyButton()
    {
        isReady = (isReady + 1) % 2;
        Debug.Log(isReady);
        
        playerProperties["playerReady"] = isReady;
        
        Debug.Log((int)playerProperties["playerReady"]);

        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }
}
