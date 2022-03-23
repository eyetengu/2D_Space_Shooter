using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUp2D : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    private Player2D _player2d;

    [SerializeField]            //0 = tripleshot 1 = speed 2 = shield
    private int _powerupID;

    private int _tripleShotPowerUp = 0;
    private int _speedPowerUp = 1;
    private int _ShieldsPowerUp = 2;

    private int _ammoPowerUp = 3;
    //private int _healthPowerUp = 4;

    [SerializeField]
    private SoundManager _soundManager;
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
            Debug.Log("PowerUP2.cs- Collision Detected");

            _soundManager.PowerUpSound();

            //if(_player2d == null)            
            //{
                switch (_powerupID)
                    {
                        case 0:
                        Debug.Log("Case 0");
                            _player2d.TripleShotActive();
                            //Debug.Log("3X Shot");
                            break;
                        case 1:
                            _player2d.SpeedBoostActive();
                            //Debug.Log("Speed");
                            break;
                        case 2:
                            _player2d.ShieldsActive();
                            //Debug.Log("Shield");
                            break;

                        case 3:
                            _player2d.AmmoIncrease();
                            break;
                        //case 4:
                            //_player2d.HealthIncrease();
                            //break;

                        default:
                        Debug.Log("default");
                            break;
                    }

            Destroy(this.gameObject);
         }
    }
}
