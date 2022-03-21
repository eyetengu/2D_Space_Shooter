using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    public SpawnManager _spawnManager;
    [SerializeField]
    private SoundManager _soundManager;

    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _speedMultiplier = 1f;
    [SerializeField]
    private float _actualSpeed;
    
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score = 0;


    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField] 
    private bool _isSpeedActive;
    [SerializeField]
    private bool _isShieldActive;

    [SerializeField]
    private GameObject _tripleShotPrefab; 
    [SerializeField]
    private GameObject _shieldVisualiser;
    [SerializeField]
    private GameObject _speedVisualiser;

    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private GameObject _rightEngine;


    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uiManager == null)
        {
            Debug.LogError("Player.cs- No UIManager found");
        }
        
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("Player.cs- Unable to Locate SpawnManager");
        }

        _soundManager = GameObject.Find("Sound_Manager").GetComponent<SoundManager>();
        if(_soundManager == null)
        {
            Debug.LogError("Player2D.cs- SoundManager not found");
        }
        
        transform.position = new Vector3(0, 0, 0);

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        PlayerMovement();

        if(Input.GetKeyDown(KeyCode.Space) && (Time.time > _canFire))
        {
            FireLaser();
        }

        if(_isShieldActive)
            {
                ShieldsActive();
            }

        if(_isSpeedActive)
            {SpeedBoostActive();}

        if(_isTripleShotActive)
            {TripleShotActive();}

    }

    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

                Vector3 velocity = direction * _speed * _speedMultiplier * Time.deltaTime;
                transform.Translate(velocity);
                _actualSpeed = _speed * _speedMultiplier;
           
       
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -5f, 0), 0);        
        
         if(transform.position.x >= 11)
         {            transform.position = new Vector3(-11, transform.position.y, 0);        }
    
         else if(transform.position.x <= -11)
         {            transform.position = new Vector3(11, transform.position.y, 0);        }

    }

    private void FireLaser()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _canFire = Time.time + _fireRate;

            if(_isTripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);  
            }
            else
            {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            }
            _soundManager.LaserSound();
        }
    }

//PowerUp Logic
    public void TripleShotActive()
    {
        Debug.Log("3xShot");

        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {      
        yield return new WaitForSeconds(4.0f);
        _isTripleShotActive = false;
        yield return new WaitForSeconds(1.0f);
    }

//
    public void SpeedBoostActive()
    {
        Debug.Log("Speed");
        _isSpeedActive = true; 
        _speedVisualiser.SetActive(true);
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        _speedMultiplier = 2;
        _isSpeedActive = false;
        yield return new WaitForSeconds(4.0f);
        _speedMultiplier = 1;
        _speedVisualiser.SetActive(false);
        yield return new WaitForSeconds(1.0f);

    }

    //
    public void ShieldsActive()
    {
        Debug.Log("Shields");

        _isShieldActive = true;
        StartCoroutine(ShieldPowerDownRoutine());
    }

    IEnumerator ShieldPowerDownRoutine()
    {
        _shieldVisualiser.SetActive(true);
        yield return new WaitForSeconds(1.0f);
    }

//Updates to the UI
    public void TakeDamage()
    {
        if(_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldVisualiser.SetActive(false);
            return;
        }

        _lives --;

        switch(_lives)
        {
            case 2:
                _leftEngine.SetActive(true);
                break;
            case 1:
                _rightEngine.SetActive(true);
                break;
            case 0:
                break;

        }

        _uiManager.UpdateHealthUI(_lives);

        if(_lives < 1)
        {
            _lives = 0;
            if(_spawnManager != null)
            {
                _spawnManager.PlayerDeath();
            }

            Destroy(this.gameObject);
        }
    }

    public void UpdateScore()
    {
        _score += 10;
        _uiManager.UpdateScoreUI(_score);
    }
}
