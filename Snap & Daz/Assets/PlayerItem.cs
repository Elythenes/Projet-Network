using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Linq;

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

    public void OnClickReadyButton()
    {
        playerProperties["isReady"] = !(bool)playerProperties["isReady"];
        
        leftArrowButton.SetActive(!leftArrowButton.activeInHierarchy);
        rightArrowButton.SetActive(!rightArrowButton.activeInHierarchy);
        
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
        
        if (player.CustomProperties.ContainsKey("isReady"))
        {
            int x = (bool)player.CustomProperties["isReady"] ? 1 : 0;
            readyZone.color = colors[x];
            playerProperties["isReady"] = (bool)player.CustomProperties["isReady"];
        }
        else
        {
            playerProperties["isReady"] = false;
        }
    }
}
