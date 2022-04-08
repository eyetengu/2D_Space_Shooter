using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour
{

    private Animator _gameCameraAnimator;
    private UIManager _uiManager;
    private SpawnManager _spawnManager;
    private SoundManager _soundManager;
    private Animator _playerAnimator;

    private int _gamePlayMessenger;

//Enemy
    [SerializeField]
    private Enemy2D _enemy2d;
    //private bool _speedUpEnemy = false;

//Player Speed
    private float _speed = 5f;
    private float _speedMultiplier = 1f;
    //[SerializeField]
    private float _actualSpeed;
    
//Laser
    [SerializeField]
    private bool _isLaserActive = true;
    [SerializeField]
    private bool _isTripleShotActive;
//Ammo Center
    [SerializeField]
    private int _ammoCount = 3;
    [SerializeField]
    private int _maxAmmoCount = 15;
    private float _hasAmmo;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab; 
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    //[SerializeField]
    //private float cdtLaser = 5f;        //CoolDownTimerLaser(cdtLaser)
//Weapon 2ndary
    [SerializeField]
    private bool _isSecondaryEquipped = false;
    [SerializeField]
    private float _blastRadius;
    [SerializeField]
    private GameObject _bombPrefab;
    [SerializeField]
    private GameObject bomb;
    //[SerializeField]
    //public float _fuse = 2f;

//Health & Score
    [SerializeField]
    private int _lives = 3;
    private int _score = 0;

    //SpeedCenter
    float fuelLevelCurrent;
        [SerializeField] 
    private bool _isSpeedActive;
    [SerializeField]
    private bool _hasFuel = false;
    [SerializeField]
    private float _fuelLevel = 0f;
    private bool _isConsuming;
    //private float _cdtThrusters = 8f;

//Shields
    [SerializeField]
    private bool _isShieldActive;
    [SerializeField]
    private int _shields = 0;

//Player VISUALIZER
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _shieldVisualiser;
    [SerializeField]
    private GameObject _speedVisualiser;

//Fuel Control

    void Start()
    {

        _isLaserActive = true;
        Debug.Log("Start " + _isLaserActive);

        _gameCameraAnimator = GameObject.Find("Main Camera").GetComponent<Animator>();
        if(_gameCameraAnimator == null )
        {
            Debug.LogError("No Camera Found");
        }

        _playerAnimator = GetComponent<Animator>();
        if(_playerAnimator == null )
        {
            Debug.LogError("Player2D- no Animator found");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uiManager == null)
        {
            Debug.LogError("Player.cs- No UIManager found");
        }
        
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
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
        FuelCheck(fuelLevelCurrent);
        PlayerMovement();
        UIUpdate();
        WeaponsStatus();
    }

    private void FuelCheck(float fuelLevelCurrent)
    {
        if(_fuelLevel > 100)
        { 
            _fuelLevel = 100;
            _hasFuel = true;
        }
        else if(_fuelLevel > 0)
        {
            _hasFuel = true;
        }
        else if (_fuelLevel < 1)
        {
            _fuelLevel = 0;
            _hasFuel = false;
        }
        Debug.Log("Fuel- " + _fuelLevel + "%");
        _uiManager.FuelManager(_fuelLevel);
    }
    public void UpdateScore()
    {
        _score += 10;
        _uiManager.UpdateScoreUI(_score);
    }
    private void UIUpdate()
    {
        _uiManager.AmmoCountUpdate(_ammoCount, _maxAmmoCount);
        _uiManager.UpdateShieldsUI(_shields);
    }

    private void PlayerMovement()
    {
        //Base Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        GetComponent<Animator>();
        if (horizontalInput < -0.1f)
        { 
            _playerAnimator.SetBool("TurnLeft", true);
            _playerAnimator.SetBool("TurnRight", false); 
        }
        else if (horizontalInput > .1f)
        {
            _playerAnimator.SetBool("TurnLeft", false);
            _playerAnimator.SetBool("TurnRight", true);
        }
        else
        {
            _playerAnimator.SetBool("TurnLeft", false);
            _playerAnimator.SetBool("TurnRight", false);
        }

        Vector3 velocity = direction * _speed * _speedMultiplier * Time.deltaTime;
        transform.Translate(velocity);
        _actualSpeed = _speed * _speedMultiplier;

        //Setting Player Bounds       
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -5f, 0), 0);

        if (transform.position.x >= 11)
        { transform.position = new Vector3(-11, transform.position.y, 0); }

        else if (transform.position.x <= -11)
        { transform.position = new Vector3(11, transform.position.y, 0); }

        //SPEED BOOST
        if (_hasFuel)
        {          
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (_isSpeedActive == false)
                {
                    Debug.Log("SpeedBoost Active");
                    _isSpeedActive = true;        
                    
                    _speedVisualiser.SetActive(true);
                    _speedMultiplier = 2;
                    _isConsuming = true;

                    StartCoroutine(FuelConsumption());
                }
                else if (_isSpeedActive == true)
                {
                    Debug.Log("SpeedBoost OFF");

                    _isSpeedActive = false;
                    _speedVisualiser.SetActive(false);
                    _speedMultiplier = 1;
                    _isConsuming = false;
                }                
            }            
        }
        else 
        {
            _isSpeedActive = false;
            _speedVisualiser.SetActive(false);
            _speedMultiplier = 1;
            _isConsuming = false;
        }
    }
    private void WeaponsStatus()
    {
        if (_isLaserActive == true && Input.GetKeyDown(KeyCode.Space) && (Time.time > _canFire))
            {
                //Debug.Log("Space pressed");
                if (_ammoCount > 0)
                {
                    _gamePlayMessenger = 0;
                    FireLaser();
                }
                else if(_ammoCount < 1)
                {
                    //_gamePlayMessenger = 1;
                }
                _uiManager.GamePlayMessages(_gamePlayMessenger);
            } 
        
        if(_isTripleShotActive)
            {   TripleShotActive(); }

        if (_isSecondaryEquipped == true && Input.GetKey(KeyCode.E))
        {
            Debug.Log("Secondary Fired");
            StartCoroutine(SecondaryFire());    
        }

    }
    private void FireLaser()
    {
        //Debug.Log("FireLaser() " + _isLaserActive);

        if (_ammoCount < 2)
        {
            _gamePlayMessenger = 1;
            _uiManager.GamePlayMessages(_gamePlayMessenger);
        }
        _canFire = Time.time + _fireRate;


        if(_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            _ammoCount -= 3;    
            if(_ammoCount < 1)
            { _ammoCount = 0; }
            _uiManager.AmmoCountUpdate(_ammoCount, _maxAmmoCount);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            _ammoCount--;
        }
        _soundManager.LaserSound();
        //Debug.Log("AmmoCount- " + _ammoCount);
    }    
    
    IEnumerator SecondaryFire()
    {
            Instantiate(_bombPrefab, transform.position , Quaternion.identity);
            
            _isSecondaryEquipped = false;     
        
            StartCoroutine(CoolDownTimerLaser());
        yield return new WaitForSeconds(.1f);
            //_explosionTransform = _bombTransform;
    }
        
    IEnumerator FuelConsumption()
    {
        while (_fuelLevel > 0 && _isConsuming == true)
        {
            _fuelLevel -= 1;
            yield return new WaitForSeconds(.25f);
        }
    }    
    IEnumerator ResetTrigger()
    {
        yield return new WaitForSeconds(1f);
        _gameCameraAnimator.SetBool("CameraShake_bool", false);
    }

//PowerUp Logic
    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    public void AcquiredSecondaryFire()
    {
        _isSecondaryEquipped = true;
    }
    public void AmmoIncrease()
    {
        _ammoCount += 15;
        if(_ammoCount > _maxAmmoCount)
        {
            _ammoCount = 15;
        }
        _isLaserActive = true;
        _gamePlayMessenger = 0;
        _uiManager.AmmoCountUpdate(_ammoCount, _maxAmmoCount);
    }
    public void AcquiredSpeedBoost()
    {
        _fuelLevel += 20;
        fuelLevelCurrent = _fuelLevel;
        FuelCheck(fuelLevelCurrent);
    }
    public void AcquiredShields()
    {                
        _shieldVisualiser.SetActive(true);

        StartCoroutine(ShieldPowerDownRoutine());
    }
    public void HealthIncrease()
    {
        _lives++;
        if (_lives > 3)
        { _lives = 3; }
        Debug.Log("Lives " + _lives);
        _uiManager.UpdateHealthUI(_lives);
        switch (_lives)
        {
            case 3:
                _leftEngine.SetActive(false);
                _rightEngine.SetActive(false);
                break;
            case 2:
                _leftEngine.SetActive(true);
                _rightEngine.SetActive(false);
                break;
            case 1:
                _leftEngine.SetActive(true);
                _rightEngine.SetActive(true);
                break;
            case 0:
                break;
        }
    }    
    public void AcquiredNegativePowerup()
    {
        TakeDamage();
    }

    IEnumerator CoolDownTimerLaser()
    {
        _isLaserActive = false;
        //yield return new WaitForSeconds(5f);


        for (int i = 0; i < 5; i++)
        {
            //_coolDownImage = i;
            Debug.Log("Timer: " + i);
            yield return new WaitForSeconds(1);
        }
        Debug.Log("_isLaserReactivated");
        _isLaserActive = true;
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        //_tripleShotPrefab;
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }
    IEnumerator ShieldPowerDownRoutine()
    {
        _shields += 3;
        if(_shields > 3)
        {
            _shields = 3;
        }
        //_uiManager.UpdateShieldsUI(_shields);      
        _isShieldActive = true;

        yield return new WaitForSeconds(1.0f);
    }

//Damage Control
    void CollateralDamage()
    {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(enemy.transform.position, bomb.transform.position);
                if(distance < _blastRadius && enemy != null)
                {
                    _enemy2d.EnemyTakeDamage();
                }
            
        }
        //yield return new WaitForSeconds(remainingCooldownTime);
        _isLaserActive = true;
        Debug.Log("CoroutineEnd" + _isLaserActive);
    }
    public void TakeDamage()
    {
        if (_isShieldActive == true && _shields > -1)
        { 
            _shields--;
            Debug.Log(_shields); 

            if(_shields == 0)
            { 
            _shieldVisualiser.SetActive(false);
            _isShieldActive = false;
            }
        } 
        else 
        {
            _lives --;
        }
        _gameCameraAnimator.SetBool("CameraShake_bool", true);
        StartCoroutine(ResetTrigger());

        switch (_lives)
        {
            case 2:
                _leftEngine.SetActive(true);
                _rightEngine.SetActive(false);
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

//Debug
    private void OnDrawGizmos() 
    {
        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(transform.position, 2f); 
    }
}
