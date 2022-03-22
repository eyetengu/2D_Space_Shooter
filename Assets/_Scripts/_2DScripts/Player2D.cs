using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    private UIManager _uiManager;
    public SpawnManager _spawnManager;
    private SoundManager _soundManager;
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

    private int _lives = 3;
    private int _score = 0;

    [SerializeField]
    private bool _isTripleShotActive;
    [SerializeField] 
    private bool _isSpeedActive;
    [SerializeField]
    private bool _isShieldActive;

    [SerializeField]
    private int _shields = 0;

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

    private float _fuelCells = 0;
    private bool _hasFuelCells = false;

    private bool _speedUpEnemy = false;

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
        
        _uiManager.UpdateFuelCellsUI(_fuelCells);

        //_enemy2d = GameObject.Find("Enemy2D").GetComponent<Enemy2D>();
        //if(_enemy2d == null)
        //{
            //Debug.LogError("Player2D.cs- Enemy2D is Null");
        //}

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

        _enemy2d.FastEnemy(false);
        

        //if(_isSpeedActive)
            //{SpeedBoostActive();}

        if(_isTripleShotActive)
            {TripleShotActive();}

        _uiManager.UpdateShieldsUI(_shields);
    }

    void PlayerMovement()
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
        if (_hasFuelCells == true && _fuelCells > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (_isSpeedActive == false)
                {
                    _isSpeedActive = true;
                    _speedVisualiser.SetActive(true);
                    _speedMultiplier = 2;
                    _speedUpEnemy = true;
                    _enemy2d.FastEnemy(_speedUpEnemy);
                    
                }

                else if (_isSpeedActive == true)
                {
                    _isSpeedActive = false;
                    _speedVisualiser.SetActive(false);
                    _speedMultiplier = 1;
                    Debug.Log("SpeedBoost IS NOT active");                    
                }                
            }
        }
        else 
        {
            _isSpeedActive = false;
            _speedVisualiser.SetActive(false);
            _speedMultiplier = 1;       
        }
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
        //_tripleShotPrefab;
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    //

    public void SpeedBoostActive()
    {
        _fuelCells += 1;
        if(_fuelCells > 5)
        { 
            _fuelCells = 5;
        }
        else
        {
            _fuelCells = _fuelCells;
        }
        Debug.Log("Fuel Check");
        _uiManager.UpdateFuelCellsUI(_fuelCells);

        _hasFuelCells = true;
        Debug.Log("SpeedCells Acquired");
    }

    public void ShieldsActive()
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
        Debug.Log("Player2d.cs- BLAHBLAH Shields at: " + _shields);
        

        _isShieldActive = true;

        yield return new WaitForSeconds(1.0f);
    }

//Updates to the UI
    public void TakeDamage()
    {
        //when an enemy collides with the player
        //check for shield, damage shield / damage player
        //check to see if shield is present
        //if yes- return without damage to the player
            //decrement the shield by 1
            //if shield is =<0
                //isShieldReady = false;
        //if no- damage player
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
                Debug.Log("Breadcrumb 3");

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
