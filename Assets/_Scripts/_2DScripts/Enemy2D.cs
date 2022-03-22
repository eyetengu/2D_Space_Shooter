using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2D : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private float enemySpeedMultiplier = 1.5f;

    [SerializeField]
    private Player2D _player;
    private Animator _animator;
    private BoxCollider2D _collider;
    private SoundManager _soundManager;

    private bool speedBool = false;


    void Start()
    {
        transform.position = new Vector3(Random.Range(-9, 9), 6, 0);

        _player = GameObject.Find("Player_2D").GetComponent<Player2D>();
        if(_player == null)
        {
            Debug.LogError("Enemy2D.cs- Player Script Not Found");
        }

        _animator = GetComponent<Animator>();
        if(_animator == null)
        {
            Debug.LogError("Enemy2D.cs- Animator not found");
        }

        _collider = GetComponent<BoxCollider2D>();
        if(_collider == null)
        {
            Debug.LogError("Enemy2d.cs- Unable to locate BoxCollider2D");
        }

        _soundManager = GameObject.Find("Sound_Manager").GetComponent<SoundManager>();
        if(_soundManager == null)
        {
            Debug.LogError("Enemy2D.cs- SoundManager Not Found");
        }
    }

    void Update()
    {
        EnemyMovement();
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
            _soundManager.ExplosionSound();
        if(other.tag == "Player")
        {
            Player2D player = other.transform.GetComponent<Player2D>();

            if(player != null)
            {
                player.TakeDamage();
            }

            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2f);
        }

        if(other.tag == "Laser")
        {
            Debug.Log("Laser Contact");
            Destroy(_collider);
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            //_audioSource.Play();
            Destroy(other.gameObject);
            _player.UpdateScore();
            Destroy(this.gameObject, 2f);
        }  
    }

    public void FastEnemy(bool _speedUpEnemy)
    {
        if(_speedUpEnemy == true)
        {
            Debug.Log("FastEnemy = true");
            _speed =6f;    
        }
        else if(_speedUpEnemy == false)
        {
            Debug.Log("FastEnemy = false");
            _speed = 4f;
        }
    }

    




}
