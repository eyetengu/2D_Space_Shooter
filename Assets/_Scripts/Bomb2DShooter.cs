using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb2DShooter : MonoBehaviour
{
    [SerializeField]
    private float _timer = 3.0f;
    [SerializeField]
    private float _secondarySpeed = 2f;
    [SerializeField]
    private float _blastRadius = 5.0f;
    [SerializeField]
    private GameObject _explosionPrefab;
    GameObject[] enemies;
    private SoundManager _soundManager;
    private Player2D _player;
    private Transform _playerTransform;


    void Start()
    {
        _player = GameObject.Find("Player_2D").GetComponent<Player2D>();        
        if( _player == null )
        {
            Debug.LogError("Player Not Found");
        }
        
        _soundManager = GameObject.Find("Sound_Manager").GetComponent<SoundManager>();        
        if(_soundManager == null )
        { Debug.Log("Bomb2dShooter.cs- SoundManager Unavailable"); }
        
        _playerTransform = _player.transform;

        Debug.Log("Bomb Deployed");

        StartCoroutine(TickTickBoom());
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.up * _secondarySpeed * Time.deltaTime);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    IEnumerator TickTickBoom()
    {
        yield return new WaitForSeconds(_timer);
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        CheckForCasualties();
        _soundManager.ExplosionSound();
        Debug.Log("Destroyed");
        Destroy(this.gameObject, .1f);


    }

    private void CheckForCasualties()
    {
        float playerDistance = Vector3.Distance(_playerTransform.position, this.transform.position);
        Debug.Log("PlayerDistance " + playerDistance + "BlastRadius " + _blastRadius);
        if (playerDistance < _blastRadius) 
        {
            _player.TakeDamage();
        }


        Debug.Log("CheckForCasualties");
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, this.transform.position);

            if (distance < _blastRadius)
            {
                Destroy(enemy.gameObject);
                Debug.Log("My life for liberty");
            }
        }
    }
}
