using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    private Player2D _player;
    [SerializeField]
    private float _speed = 2.0f;
    [SerializeField]
    private int _shockwaveDir;
    [SerializeField]
    private GameObject _explosionPrefab;

    private Animator _gameCameraAnimator;
    private SoundManager _soundManager;

    Vector3 _northEast = new Vector3(1, 1, 0);
    Vector3 _southEast = new Vector3(1, -1, 0);
    Vector3 _southWest = new Vector3(-1, -1, 0);
    Vector3 _northWest = new Vector3(-1, 1, 0);

    void Start()
    {
        _northEast = Vector3.Normalize(_northEast);
        _northWest = Vector3.Normalize(_northWest);
        _southEast = Vector3.Normalize(_southEast);
        _southEast = Vector3.Normalize(_southEast);

        _player = GameObject.Find("Player_2D").GetComponent<Player2D>();
        _gameCameraAnimator = GameObject.Find("Main Camera").GetComponent<Animator>();
        _soundManager = GameObject.Find("Sound_Manager").GetComponent<SoundManager>();
    }

    void Update()
    {
        ShockwaveMovement();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Debug.Log("HAHA- Take That");
            _player.TakeDamage();
            _gameCameraAnimator.SetTrigger("CameraShake_trigger");
            _soundManager.ExplosionSound();
            _gameCameraAnimator.ResetTrigger("CameraShake_trigger");

            Destroy(this.gameObject, .1f);
        }
    }

    void ShockwaveMovement()
    {
        if (_shockwaveDir > 7)
            _shockwaveDir = 7;
        if (_shockwaveDir < 0)
            _shockwaveDir = 0;

        switch (_shockwaveDir)
        {
            case 0:
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
                break;
            case 1:
                transform.Translate(_northEast * _speed * Time.deltaTime);

                break;
            case 2:
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
                break;
            case 3:
                transform.Translate(_southEast * _speed * Time.deltaTime);
                break;
            case 4:
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
                break;
            case 5:
                transform.Translate(_southWest * _speed * Time.deltaTime);
                break;
            case 6:
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
                break;
            case 7:
                transform.Translate(_northWest * _speed * Time.deltaTime);
                break;
            default:
                break;
        }
        Destroy(this.gameObject, 3f);
    }

}
