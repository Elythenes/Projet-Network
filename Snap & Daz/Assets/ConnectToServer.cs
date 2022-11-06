using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public TMP_InputField usernameInput;
    public Button connectButton;
    public TextMeshProUGUI buttonText;

    void Start()
    {
        string defaultName = string.Empty;
        
        if (usernameInput != null)
        {
            if (PlayerPrefs.HasKey("PlayerName"))
            {
                defaultName = PlayerPrefs.GetString("PlayerName");
                usernameInput.text = defaultName;
            }
        }

        PhotonNetwork.NickName = defaultName;
    }
    
    void Update()
    {
        if (usernameInput.text.Length == 0)
        {
            connectButton.interactable = false;
        }
        else
        {
            connectButton.interactable = true;
        }
    }

    public void OnClickConnect()
    {
        if (usernameInput.text.Length >= 1)
        {
            PhotonNetwork.NickName = usernameInput.text;
            PlayerPrefs.SetString("PlayerName", PhotonNetwork.NickName);
            buttonText.text = "Connecting...";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
    }
}
