using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
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
    public Button readyButton;

    private ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    public Image playerAvatar;
    public Sprite[] avatars;
    
    public Image readyZone;
    public Color[] colors = {Color.white, new Color(181f / 255f, 127f / 255f, 127f / 255f) };

    public Player player;
    private bool isReady;

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
        readyButton.interactable = true;
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

        foreach (Player tempPlayer in PhotonNetwork.PlayerList)
        {
            if (Equals(tempPlayer, PhotonNetwork.LocalPlayer)) return;

            if ((int)tempPlayer.CustomProperties["playerAvatar"] != (int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"] && (bool)tempPlayer.CustomProperties["isReady"] && (bool)PhotonNetwork.LocalPlayer.CustomProperties["isReady"])
            {
                playerProperties["canStart"] = true;
                tempPlayer.CustomProperties["canStart"] = true;
                PhotonNetwork.SetPlayerCustomProperties(playerProperties);
            }
            else
            {
                playerProperties["canStart"] = false;
                tempPlayer.CustomProperties["canStart"] = false;
                PhotonNetwork.SetPlayerCustomProperties(playerProperties);
            }
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

        if (player.CustomProperties.ContainsKey("isReady"))
        {
            readyZone.color = colors[(bool)player.CustomProperties["isReady"]?0:1];
            playerProperties["isReady"] = (bool)player.CustomProperties["isReady"];
        }
        else
        {
            playerProperties["isReady"] = false;
        }
    }

    public void OnClickReadyButton()
    {
        isReady = !isReady;

        playerProperties["isReady"] = isReady;
        
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }
}
