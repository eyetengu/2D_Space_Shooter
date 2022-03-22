using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    private UIManager _uiManager;
    public SpawnManager _spawnManager;
    private SoundManager _soundManager;

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

        //Debug.Log("Player2d.cs- Shields at: " + _shields);

    }

    void Update()
    {
        PlayerMovement();

        if(Input.GetKeyDown(KeyCode.Space) && (Time.time > _canFire))
        {
            FireLaser();
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            _isSpeedActive = true;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            _isSpeedActive = false;
        }

        if(_isSpeedActive)
            {SpeedBoostActive();}

        if(_isTripleShotActive)
            {TripleShotActive();}

        _uiManager.UpdateShieldsUI(_shields);
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
        //_tripleShotPrefab;
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    //

    public void SpeedBoostActive()
    {
        //Debug.Log("Speed");
        _isSpeedActive = true; 
        _speedVisualiser.SetActive(true);
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }
    IEnumerator SpeedBoostPowerDownRoutine()
    {
        _speedMultiplier = 2;
        yield return new WaitForSeconds(4.0f);
        _isSpeedActive = false;
        _speedMultiplier = 1;
        _speedVisualiser.SetActive(false);
        yield return new WaitForSeconds(1.0f);
    }

    //

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
