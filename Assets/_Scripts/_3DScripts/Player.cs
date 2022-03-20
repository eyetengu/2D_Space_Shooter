using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private UIManager _uiManager;

    [SerializeField]
    private float _speed = 3.5f;
    
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private int _health = 100;
    [SerializeField]
    private int _score = 0;
    private int _halfScore;

    public SpawnManager _spawnManager;

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uiManager == null)
        {
            Debug.Log("No UIManager found");
        }
        else 
        {
            Debug.Log("UIManager Located and Activated");
        }

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.Log("Unable to Locate SpawnManager");
        }
        else {
            Debug.Log("SpawnManager Present and Accounted For");
        }
        
        transform.position = new Vector3(0, 0, 0);
        Debug.Log("Lives: " + _lives);
    }

    void Update()
    {
        PlayerMovement();

        if(Input.GetKeyDown(KeyCode.Space) && (Time.time > _canFire))
        {
            FireLaser();
        }
    }

    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        Vector3 velocity = direction * _speed * Time.deltaTime;
        transform.Translate(velocity);
       
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -5f, 0), 0);        
        
         if(transform.position.x >= 11)
         {            transform.position = new Vector3(-11, transform.position.y, 0);        }
    
         else if(transform.position.x <= -11)
         {            transform.position = new Vector3(11, transform.position.y, 0);        }

    }

    private void FireLaser()
    {
            _canFire = Time.time + _fireRate;
            Debug.Log("Pew Pew");
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);

    }

    public void TakeDamage()
    {
        _lives -= 1;

        if(_lives <= 0)
        {
            _lives = 0;
            if(_spawnManager != null)
            {
            _spawnManager.PlayerDeath();
            }

            Destroy(this.gameObject);
        }
        else {
        _uiManager.UpdateHealthUI(_lives);
        }
    }
    public void UpdateScore()
    {
        _score += 1;
        _halfScore = _score / 2;
        _uiManager.UpdateScoreUI(_score);
    }

}
