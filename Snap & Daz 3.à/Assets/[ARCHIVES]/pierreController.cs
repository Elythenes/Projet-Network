/*using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
  [SerializeField] private int playerIndex;
  
  private Rigidbody _rb;
  [SerializeField] private LineRenderer _lineRenderer;
  
  [Header("Mouvement")]
  [SerializeField] private float moveSpeed;
  private Vector2 _moveValue;
  private bool _isMoving;

  private Quaternion _currentRotation;
  [SerializeField, Range(0,0.1f)] private float rotationSpeed;

  [Header("Shooting")]
  [SerializeField] private float aimSensitivity;
  private bool _isAiming;

  [SerializeField] private float shootSensitivity;
  private bool _isShooting;

  [Space, SerializeField] private Transform canon1;
  [SerializeField] private Transform canon2;
  
  [Space, SerializeField] private int maxAmmoInReserve;
  [SerializeField] private int currentAmmoInReserve;
  [SerializeField] private int maxAmmoInMag;
  [SerializeField] private int currentAmmoInMag;

  [SerializeField] private float reloadTime;
  private bool _isReloading;

  [Space, SerializeField] private float cooldownBetweenShoots;
  private bool _justShoot;

  [Header("Ability")]
  [SerializeField] private int neededAbilityPoints;
  [SerializeField] private int currentAbilityPoints;

  private GameObject interactibleElement;

  [Header("VFX")]
  [SerializeField] private ParticleSystem shootright;
  [SerializeField] private ParticleSystem shootleft;

  protected override void Awake()
  { 
      base.Awake();
      _rb = GetComponent<Rigidbody>();
      currentAbilityPoints = neededAbilityPoints;
      currentAmmoInMag = maxAmmoInMag;
      currentAmmoInReserve = maxAmmoInReserve;
  }

  protected override void Start()
  {
    base.Start();
    UIManager.instance.UpdatePlayerAmmo(playerIndex-1, currentAmmoInMag, currentAmmoInReserve);
  }

  public LayerMask layers;

  private void FixedUpdate()
  {
    if (GameManager.instance.gameIsPaused) return;


    RaycastHit hitForLine;
    if (Physics.Raycast(canon2.position, (canon2.position - canon1.position).normalized, out hitForLine,
          Mathf.Infinity, ~layers))

  {

      _lineRenderer.SetPosition(0, canon2.position);
      _lineRenderer.SetPosition(1, hitForLine.point);
  }
    
    ManageMovement(); 
    ManageShooting();
    ShootingEffect();
  }

  private void ManageMovement()
  {
    if (!_isAiming && _isMoving)
    {
      _rb.velocity = transform.forward * moveSpeed;
      animator.SetBool("isWalking", true);
    }
    else
    {
      _rb.velocity = Vector3.zero;
      animator.SetBool("isWalking", false);
    }
  }

  private void ManageShooting()
  {
    if (currentAmmoInMag is 0 && !_isReloading)
    {
      StartCoroutine(Realoding());
      return;
    }
    
    if (!_isShooting || _isReloading || _justShoot) return;

    _justShoot = true;
  
    
    Vector3 canonPos = canon1.position;
    Vector3 dir = canonPos - transform.position;
    
    RaycastHit hit;
    if (Physics.Raycast(canonPos, dir.normalized, out hit, Mathf.Infinity))
    {
      

      if (hit.collider.CompareTag("Enemy"))
      {
        Debug.DrawRay(canonPos, dir.normalized * hit.distance, Color.red);
        hit.collider.GetComponent<BaseEntity>().currentHealthPoints -= damage;
      }
    }

    currentAmmoInMag--;
    UIManager.instance.UpdatePlayerAmmo(playerIndex-1, currentAmmoInMag, currentAmmoInReserve);
    
    StartCoroutine(WaitBeforeShootingAgain());
  }

  private void ShootingEffect()
  {
    if (!_isShooting || _isReloading)
    {
      shootright.gameObject.SetActive(false);
      shootleft.gameObject.SetActive(false);
    }
    else
    {
      shootleft.gameObject.SetActive(true);
      shootright.gameObject.SetActive(true);
    }
  }

  private IEnumerator WaitBeforeShootingAgain()
  {
    yield return new WaitForSeconds(cooldownBetweenShoots);
    _justShoot = false;
  }

  private void OnMove(InputValue value)
  {
    if (gameManager.gameIsPaused) return;
      
    if (value.Get<Vector2>() != Vector2.zero)
    {
      _isMoving = true;
      _moveValue = value.Get<Vector2>();
      Quaternion lookRotation = Quaternion.LookRotation(new Vector3(_moveValue.x, 0, _moveValue.y));
     // transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotationSpeed);
     transform.rotation = lookRotation;
    }
    else _isMoving = false;
  }

  private void OnAim(InputValue value)
  {
    _isAiming = value.Get<float>() > aimSensitivity;
  }

  private void OnShoot(InputValue value)
  {
    if (gameManager.gameIsPaused) return;
    
    _isShooting = value.Get<float>() > shootSensitivity;
  }

  private void OnInteract()
  {
    if (gameManager.gameIsPaused) return;

    if (interactibleElement is null) return;

    interactibleElement.GetComponent<ActivatedElements>().Activation(this);

    interactibleElement = null;
  }

  private void OnReload()
  {
    if (gameManager.gameIsPaused) return;

    if (_isReloading) return;
    StartCoroutine(Realoding());
  }

  private void OnPause()
  {
    if (gameManager.gameIsPaused)
    {
      gameManager.gameIsPaused = false;
      Time.timeScale = 1;
      UIManager.instance.pausePanel.SetActive(false);
    }
    else
    {
      gameManager.gameIsPaused = true;
      Time.timeScale = 0;
      UIManager.instance.pausePanel.SetActive(true);
    }
  }

  private IEnumerator Realoding()
  {
    _isReloading = true;
    yield return new WaitForSeconds(reloadTime);
    
    if (currentAmmoInReserve > maxAmmoInMag)
    {
      currentAmmoInReserve -= maxAmmoInMag;
      int bulletsMissing = maxAmmoInMag - currentAmmoInMag;
      currentAmmoInMag += bulletsMissing;
    }
    
    UIManager.instance.UpdatePlayerAmmo(playerIndex-1, currentAmmoInMag, currentAmmoInReserve);
    _isReloading = false;
  }

  public void PlayerLoseHp(int damage)
  {
    currentHealthPoints -= damage;
    UIManager.instance.playersHealthSliders[playerIndex - 1].value = currentHealthPoints/maxHealthPoints*100;
    if (currentHealthPoints <= 0)
    {
      gameManager.activePlayers.Remove(transform);
      if (gameManager.activePlayers.Count is 0) gameManager.GameOver();
      gameManager.UpdateAllEnemiesTarget(gameManager.activePlayers[Random.Range(0,gameManager.activePlayers.Count)]);
      gameObject.SetActive(false);
    }
  }

  public void PlayerGainHp(int heal) // Regagner de la vie avec les médipacks
  {
    currentHealthPoints += heal;
    if (currentHealthPoints > maxHealthPoints)
    {
      currentHealthPoints = maxHealthPoints;
    }
    UIManager.instance.playersHealthSliders[playerIndex - 1].value = currentHealthPoints/maxHealthPoints*100;
  }

  public void PlayerGainAmmos(int ammos) // Regagner des munitions avec les packs de munitions
  {
    currentAmmoInReserve += ammos;
    if (currentAmmoInReserve > maxAmmoInReserve)
    {
      currentAmmoInReserve = maxAmmoInReserve;
    }
    
    UIManager.instance.UpdatePlayerAmmo(playerIndex-1, currentAmmoInMag, currentAmmoInReserve);
  }

  // Détecter les éléments interactibles à portée
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Interactible"))
    {
      interactibleElement = other.gameObject;
    }
  } 

  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Interactible"))
    {
      interactibleElement = null;
    }
  } 
}*/
