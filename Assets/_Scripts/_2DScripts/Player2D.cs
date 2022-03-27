using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    private UIManager _uiManager;
    public SpawnManager _spawnManager;
    private SoundManager _soundManager;

    public int _gamePlayMessenger;

    [SerializeField]
    private Enemy2D _enemy2d;

    private float _speed = 5f;
    private float _speedMultiplier = 1f;
    [SerializeField]
    private float _actualSpeed;
    
    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _ammoCount = 3;
    private float _hasAmmo;

    [SerializeField]
    private int _lives = 3;
    private int _score = 0;

    [SerializeField]
    private bool _isLaserActive = true;
    [SerializeField]
    private bool _isTripleShotActive;
    [SerializeField] 
    private bool _isSpeedActive;
    [SerializeField]
    private bool _isShieldActive;

    //Weapon 2ndary
    [SerializeField]
    private GameObject _bombPrefab;
    [SerializeField]
    private GameObject _explosionPrefab;
    private GameObject explosion;
    private GameObject bomb;

    private Transform _bombTransform;
    private Transform _explosionTransform;


    [SerializeField]
    private bool _isSecondaryEquipped = false;
    [SerializeField]
    private float cdtLaser = 5f;        //CoolDownTimerLaser(cdtLaser)


    [SerializeField]
    public float _fuse = 2f;
    private float _blastRadius;



    [SerializeField]
    private int _shields = 0;
    [SerializeField]
    private GameObject _shieldVisualiser;
    [SerializeField]
    private GameObject _speedVisualiser;

    [SerializeField]
    private GameObject _tripleShotPrefab; 

    //DAMAGE VISUALIZER
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private GameObject _rightEngine;

    [SerializeField]
    private int _fuelLevel = 0;
    [SerializeField]
    private int _fuelCells = 0;
    [SerializeField]
    private bool _hasFuelCells = false;
    private float _cdtThrusters = 8f;


    //private bool _speedUpEnemy = false;

    void Start()
    {
        _isLaserActive = true;
        Debug.Log("Start " + _isLaserActive);

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
        
        _uiManager.FuelManager(_fuelLevel, _fuelCells);

        transform.position = new Vector3(0, 0, 0);

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        FuelCheck();
        PlayerMovement();
        UIUpdate();
        WeaponsStatus();
    }

    private void FuelCheck()
    {
        if(_fuelCells > 5)
        {
            _fuelCells = 5;
        }

        if(_fuelCells > 0)
        {   
            _hasFuelCells = true;
        }
        else if( _fuelCells < 1) 
        { 
            _hasFuelCells = false; 
        }
        
        if(_fuelLevel > 100)
        { _fuelLevel = 100;}    
        else if (_fuelLevel < 1)
        {
            _fuelLevel = 0;
        }

        _uiManager.FuelManager(_fuelLevel, _fuelCells);
    }

    private void PlayerMovement()
    {
        //Base Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

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
        if (_hasFuelCells == true)
        {
            GameObject[] enemies;
            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {

                if (_isSpeedActive == false)
                {                    
                    _isSpeedActive = true;
                    _speedVisualiser.SetActive(true);
                    _speedMultiplier = 2;
                    StartCoroutine(FuelConsumption());
                }

                else if (_isSpeedActive == true)
                {
                    
                    _isSpeedActive = false;
                    _speedVisualiser.SetActive(false);
                    _speedMultiplier = 1;
                    _fuelCells--;
                    StartCoroutine(SpeedCooldown());
                }                
            }
            
        }
        else 
        {
            _isSpeedActive = false;
            _speedVisualiser.SetActive(false);
            _speedMultiplier = 1;       
        }

        _uiManager.FuelManager(_fuelLevel, _fuelCells);
    }

    private void UIUpdate()
    {
        _uiManager.AmmoCountUpdate(_ammoCount);
        _uiManager.UpdateShieldsUI(_shields);
        _uiManager.FuelManager(_fuelLevel, _fuelCells);
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
            {   SecondaryFire();    }

    }

    private void FireLaser()
    {
        Debug.Log("FireLaser() " + _isLaserActive);

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
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            _ammoCount--;
        }
        _soundManager.LaserSound();
        //Debug.Log("AmmoCount- " + _ammoCount);
    }    

    private void SecondaryFire()
    {
            bomb = Instantiate(_bombPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            
            _isSecondaryEquipped = false;     
        
            StartCoroutine(CoolDownTimerLaser());

            //_explosionTransform = _bombTransform;
    }

        
    IEnumerator FuelConsumption()
    {
        while (_fuelLevel > 0)
        {
            _fuelLevel -= 1;
            _uiManager.FuelManager(_fuelLevel, _fuelLevel);
            yield return new WaitForSeconds(.25f);
        }
        if (_fuelLevel < 1) ;
        {
            StartCoroutine(SpeedCooldown());
        }
    }
    IEnumerator SpeedCooldown()
    {
        _isSpeedActive = false;
        _speedVisualiser.SetActive(false);
        _speedMultiplier = 1;

        yield return new WaitForSeconds(_cdtThrusters);

        _isSpeedActive = false;
    }


    //PowerUp Logic
    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        //_tripleShotPrefab;
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }
    //
    public void AcquiredSpeedBoost()
    {
        _fuelCells ++;
        _fuelLevel += 20;
        FuelCheck();

        //_hasFuelCells = true;
    }
    //
    public void AcquiredShields()
    {                
        _shieldVisualiser.SetActive(true);

        StartCoroutine(ShieldPowerDownRoutine());
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
    //
    public void AmmoIncrease()
    {
        _ammoCount = 15;
        if(_ammoCount > 15)
        {
            _ammoCount = 15;
        }
        _isLaserActive = true;
        _gamePlayMessenger = 0;

    }
    //
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
    //
    public void AcquiredSecondaryFire()
    {
        _isSecondaryEquipped = true;
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

    //Updates to the UI
    public void TakeDamage()
    {
        if(_isShieldActive == true && _shields > -1)
        { 
            _shields--;
            Debug.Log(_shields); 

            if(_shields == 0)
            { 
            _shieldVisualiser?.SetActive(false);
            _isShieldActive = false;
            }
        } 
        else 
        {
            _lives --;
        }

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

    public void UpdateScore()
    {
        _score += 10;
        _uiManager.UpdateScoreUI(_score);
    }


    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 2f); 
    }
}
