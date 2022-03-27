using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUp2D : MonoBehaviour
{
    //[SerializeField]
    private SoundManager _soundManager;
    private Player2D _player2d;

    [SerializeField]
    private float _speed = 3f;

    [SerializeField]            //0 = tripleshot 1 = speed 2 = shield
    private int _powerupID;
    private int _tripleShotPowerUp = 0;
    private int _speedPowerUp = 1;
    private int _ShieldsPowerUp = 2;

    private int _ammoPowerUp = 3;
    //private int _healthPowerUp = 4;
    private int _bombPowerUp = 5;

    //private AudioClip _clip;

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

            //if(_player2d == null)            
            //{
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

                        default:
                            break;
                    }

            Destroy(this.gameObject);
         }
    }
}
