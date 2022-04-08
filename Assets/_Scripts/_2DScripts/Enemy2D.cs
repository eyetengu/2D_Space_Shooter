using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class Enemy2D : MonoBehaviour
{
//Enemy Components
    [SerializeField]
    private Animator _gameCameraAnimator;
    private Animator _animator;
    private BoxCollider2D _collider;
    private SoundManager _soundManager;

    [SerializeField]
    private Player2D _player;
    
    private Transform _enemyContainer;

//Enemy Speed Parameters
    [SerializeField]
    private float _speed = 4.0f;
    private float _enemySpeedMultiplier = 3f;
    [SerializeField]
    private float _currentSpeed;

    //enemy Shield
    private bool _enemyShieldActive;
    private int _randomShieldState = 0;
    [SerializeField]
    private GameObject _enemyShield;


    //Prototype Variables
    //private bool speedBool = false;

    void Start()
    {
        transform.position = new Vector3(Random.Range(-9, 9), 6, 0);

        _enemyContainer = GameObject.Find("EnemyContainer").GetComponent<Transform>();
        if (_enemyContainer == null)
        {
            Debug.Log("Enemy2D.sc- EnemyContainer not found");
        }
        else { this.transform.SetParent(_enemyContainer); }
        _gameCameraAnimator = GameObject.Find("Main Camera").GetComponent<Animator>();
        if (_gameCameraAnimator == null)
        {
            Debug.LogError("Enemy2D- Camera not found");
        }
        _player = GameObject.Find("Player_2D").GetComponent<Player2D>();
        if (_player == null)
        {
            Debug.LogError("Enemy2D.cs- Player Script Not Found");
        }
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Enemy2D.cs- Animator not found");
        }
        _collider = GetComponent<BoxCollider2D>();
        if (_collider == null)
        {
            Debug.LogError("Enemy2d.cs- Unable to locate BoxCollider2D");
        }
        _soundManager = GameObject.Find("Sound_Manager").GetComponent<SoundManager>();
        if (_soundManager == null)
        {
            Debug.LogError("Enemy2D.cs- SoundManager Not Found");
        }

        _randomShieldState = Random.Range(1, 100);
        if (_randomShieldState > 60)
        {
            _enemyShieldActive = true;
            _enemyShield.SetActive(true);
        }
        else
        {
            _enemyShieldActive = false;
            _enemyShield.SetActive(false);
        }
    }

    void Update()
    {
        EnemyMovement();
        _currentSpeed = _speed;
    }

    void EnemyMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y <= -7)
        {
            float randomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomX, 6, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(_soundManager != null)    
        _soundManager.ExplosionSound();
        
        Player2D player = other.transform.GetComponent<Player2D>();
        if(other.tag == "Player")
        {
            _gameCameraAnimator.SetTrigger("CameraShake_trigger");
            //_gameCameraAnimator.ResetTrigger("CameraShake_trigger");

            if (player != null)
            {
                player.TakeDamage();
            }

            _soundManager.ExplosionSound();
            _animator.SetTrigger("OnEnemyDeath");            
            Destroy(this.gameObject, 2f);
        }
        if(other.tag == "Laser")
        {
            if( _enemyShieldActive == true)
            {
                _enemyShieldActive = false;
                _enemyShield.SetActive(false);
            }

            else if (_enemyShieldActive == false)
            {
                //Debug.Log("Laser Contact");
                _player.UpdateScore();

                _collider.enabled = false;
                _animator.SetTrigger("OnEnemyDeath");
            
                Destroy(other.gameObject);
                Destroy(this.gameObject, 1f);
            }
        }  
    }

    public void FastEnemy(bool _speedUpEnemy)
    {
        Debug.Log("FastEnemy");
        if(_speedUpEnemy == true)
        {
            Debug.Log("FastEnemy = true");
            _currentSpeed = _speed * _enemySpeedMultiplier;    
        }
        
        if(_speedUpEnemy == false)
        {
            Debug.Log("FastEnemy = false");
            _currentSpeed = _speed;
        }
    }

    public void EnemyTakeDamage()
    {
        _animator.SetTrigger("OnEnemyDeath");
        Destroy(this.gameObject);
    }
}
