using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativePowerUp2D : MonoBehaviour
{
    private SoundManager _soundManager;
    private Player2D _player2d;
    private float _speed = 1;
    [SerializeField]
    private GameObject _explosionPrefab;
    
    void Start()
    {
        _soundManager = GameObject.Find("Sound_Manager").GetComponent<SoundManager>();
        _player2d = GameObject.Find("Player_2D").GetComponent<Player2D>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _soundManager.ExplosionSound();

            _player2d.TakeDamage();
            Destroy(this.gameObject);
        }
        
        else if(other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _soundManager.ExplosionSound();
            Destroy(this.gameObject);

        }
    }
}
