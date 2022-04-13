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
    private CircleCollider2D _circleCollider2D;
    
    void Start()
    {
        _soundManager = GameObject.Find("Sound_Manager").GetComponent<SoundManager>();
            if(_soundManager == null )
        { Debug.LogError("NegPU- Sound Manager Missing"); }
        _player2d = GameObject.Find("Player_2D").GetComponent<Player2D>();
            if(_player2d == null)
        { Debug.LogError("NegPU- Player Missing"); }
        _circleCollider2D = GetComponent<CircleCollider2D>();
            if(_circleCollider2D == null)
        { Debug.LogError("NegPowerUp- No Collider found"); }
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _player2d.TakeDamage();
            StartCoroutine(NegativePowerUpDestroyed());
        }
        else if (other.tag == "Laser" || other.tag == "EnemyLaser")
        {
            Destroy(other.gameObject);
            StartCoroutine(NegativePowerUpDestroyed());
        }        
    }

    IEnumerator NegativePowerUpDestroyed()
    {
        _circleCollider2D.enabled = false;
        _soundManager.ExplosionSound();
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        yield return new WaitForSeconds(.1f);
    }

}
