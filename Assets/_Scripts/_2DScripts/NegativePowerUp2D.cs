using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativePowerUp2D : MonoBehaviour
{
    private SoundManager _soundManager;
    private Player2D _player2d;
    private Animator _thisAnimator;
    [SerializeField]
    private float _speed = 2f;

    //[SerializeField]            //0 = 3xShot, 1 = speed, 2 = shield, 3 = ammo, 4 = health, 5 = secondaryFire, 6 = negativePowerup
    //private int _negativeID;

    [SerializeField]
    private GameObject _explosionPrefab;
    private CircleCollider2D _circleCollider;

    void Start()
    {
        _player2d = GameObject.Find("Player_2D").GetComponent<Player2D>();
        if (_player2d == null)
        {
            Debug.LogError("PowerUp2D.cs- Player2D is missing in action");
        }
        _thisAnimator = GetComponent<Animator>();
        if( _thisAnimator == null)
        {
            Debug.LogError("NegPowerup- no animator found");
        }
        _soundManager = GameObject.Find("Sound_Manager").GetComponent<SoundManager>();
        if (_soundManager == null)
        {
            Debug.LogError("PowerUp2D.cs- SoundManager Not Found");
        }
        _circleCollider = GetComponent<CircleCollider2D>();
        if (_circleCollider == null)
        {
            //Debug.LogError("PowerUp2D- CircleCollider not found");
        }

        transform.position = new Vector3(Random.Range(-8, 8), 6, 0);
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -7f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //_soundManager.ExplosionSound();
        //Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
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
        //_circleCollider2D.enabled = false;
        Debug.Log("NegativePowerUp Destroyed");
        _soundManager.ExplosionSound();
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject, .1f);
        yield return new WaitForSeconds(.1f);
    }

}
