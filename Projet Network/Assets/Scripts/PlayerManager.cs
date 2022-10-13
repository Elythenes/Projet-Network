using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;

public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{
    [Tooltip("The Beams GameObject to control")] [SerializeField]
    private GameObject beams;

    private bool IsFiring;

    [Tooltip("The current health of our player")]
    public float health = 1f;

    [Tooltip("The local player instance. Use this to know if the local player is represented in the scene")]
    public static GameObject LocalPlayerInstance;
    
    #region IPunObservable implementaion

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(IsFiring);
            stream.SendNext(health);
        }
        else
        {
            this.IsFiring = (bool)stream.ReceiveNext();
            this.health = (float)stream.ReceiveNext();
        }
    }
    
    #endregion

    void Awake()
    {
        if (beams == null)
        {
            Debug.LogError("<Color=Red><a>Missing>/a></Color> Beams Reference.", this);
        }
        else
        {
            beams.SetActive(false);
        }

        if (photonView.IsMine)
        {
            PlayerManager.LocalPlayerInstance = this.gameObject;
        }
        
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();
        
        if (_cameraWork != null)
        {
            if (photonView.IsMine)
            {
                _cameraWork.OnStartFollowing();
            }
        }
        else
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab");
        }
        
        #if UNITY_5_4_OR_NEWER
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        #endif
    }

    void Update()
    {

        if (photonView.IsMine)
        {
            ProcessInputs();
            if (health <= 0)
            {
                GameManager.Instance.LeaveRoom();
            }
        }

        if (beams is not null && IsFiring != beams.activeInHierarchy)
        {
            beams.SetActive(IsFiring);
        }
    }

    void ProcessInputs()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!IsFiring)
            {
                IsFiring = true;
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            if (IsFiring)
            {
                IsFiring = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (!other.name.Contains("Beam"))
        {
            return;
        }

        health -= 0.1f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (!other.name.Contains("Beam"))
        {
            return;
        }

        health -= 0.1f * Time.deltaTime;
    }
    
    #if UNITY_5_4_OR_NEWER
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
    {
        this.CalledOnLevelWasLoaded(scene.buildIndex);
    }
    #endif
    
    #if !UNITY_5_4_OR_NEWER
    void OnLevelWasLoaded(int level)
    {
        this.CalledOnLevelWasLoaded(level);
    }
    #endif
    
    void CalledOnLevelWasLoaded(int level)
    {
        // check if we are outside the Arena and if it's the case, spawn around the center of the arena in a safe zone
        if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
        {
            transform.position = new Vector3(0f, 5f, 0f);
        }
    }
    
    #if UNITY_5_4_OR_NEWER
    public override void OnDisable()
    {
        base.OnDisable ();
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #endif
}
