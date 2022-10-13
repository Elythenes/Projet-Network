using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Tooltip("UI Text to display Player's Name")] [SerializeField]
    private TextMeshProUGUI playerNameText;

    [Tooltip("UI Slider to display Player's Health")] [SerializeField]
    private Slider playerHealthSlider;

    private PlayerManager target;

    [Tooltip("Pixel offset from the player target")] [SerializeField]
    private Vector3 screenOffset = new Vector3(0f, 30f, 0f);
    
    float characterControllerHeight = 0f;
    Transform targetTransform;
    Renderer targetRenderer;
    CanvasGroup _canvasGroup;
    Vector3 targetPosition;
    

    public void SetTarget(PlayerManager _target)
    {
        if (_target == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a><Color> PlayerManager target for PlayerUI.SetTarget", this);
            return;
        }

        target = _target;

        targetTransform = this.target.GetComponent<Transform>();
        targetRenderer = this.target.GetComponent<Renderer>();
        CharacterController characterController = _target.GetComponent<CharacterController>();

        if (characterController != null)
        {
            characterControllerHeight = characterController.transform.position.y + 15f;
        }
        
        if (playerNameText != null)
        {
            playerNameText.text = target.photonView.Owner.NickName;
        }
    }

    void Awake()
    {
        this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
        _canvasGroup = this.GetComponent<CanvasGroup>();
    }
    
    void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }
        
        if (playerHealthSlider != null)
        {
            playerHealthSlider.value = target.health;
        }
    }

    private void LateUpdate()
    {
        if (targetRenderer != null)
        {
            this._canvasGroup.alpha = targetRenderer.isVisible ? 1f : 0f;
        }

        if (targetTransform != null)
        {
            targetPosition = targetTransform.position + new Vector3(0, 1.5f, 0);
            targetPosition.y += characterControllerHeight;
            this.transform.position = Camera.main.WorldToScreenPoint(targetPosition) + screenOffset;
        }
    }
}
