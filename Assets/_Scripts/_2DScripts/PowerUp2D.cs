using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUp2D : MonoBehaviour
{
    private SoundManager _soundManager;
    private Player2D _player2d;

    [SerializeField]
    private float _speed = 3f;

    [SerializeField]            //0 = 3xShot, 1 = speed, 2 = shield, 3 = ammo, 4 = health, 5 = secondaryFire, 6 = negativePowerup
    private int _powerupID;

    [SerializeField]
    private GameObject _explosionPrefab;


    void Start()
    {   
        _player2d = GameObject.Find("Player_2D").GetComponent<Player2D>();
        if(_player2d == null)
        {
            Debug.LogError("PowerUp2D.cs- Player2D is missing in action");
        }
        _soundManager = GameObject.Find("Sound_Manager").GetComponent<SoundManager>();
        if(_soundManager == null)
        {
            Debug.LogError("PowerUp2D.cs- SoundManager Not Found");
        }

        transform.position = new Vector3(Random.Range(-8, 8), 6, 0);
    }    

    void Update()
    {
         transform.Translate(Vector3.down * _speed * Time.deltaTime);

         if(transform.position.y < -7f)
         {
            Destroy(this.gameObject);
         }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _soundManager.PowerUpSound();

            switch (_powerupID)
                {
                    case 0:
                        _player2d.TripleShotActive();
                        break;
                    case 1:
                        _player2d.AcquiredSpeedBoost();
                        break;
                    case 2:
                        _player2d.AcquiredShields();
                        break;
                    case 3:
                        _player2d.AmmoIncrease();
                        break;
                    case 4:
                        _player2d.HealthIncrease();
                        break;
                    case 5:
                        _player2d.AcquiredSecondaryFire();
                        break;
                    case 6:
                        _player2d.AcquiredNegativePowerup();
                        break;

                    default:
                        break;
                }

            Destroy(this.gameObject);
        }
        if(other.tag == "EnemyLaser")
        {
            Debug.Log("Powerup hit by EnemyLaser");
            //_soundManager.ExplosionSound();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject, 1f);

        }
    }
}
