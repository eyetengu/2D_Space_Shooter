using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO_Drone : MonoBehaviour
{
    private float _speed = 4.0f;
    [SerializeField]
    //private Transform _dronePosition;

    private int _movementDir = 0;

    private float randomTime;

    private Player2D _player;
    [SerializeField]
    private float _maxTime = 3.5f;

    private Vector3 _northEast = new Vector3(1, 1, 0);
    private Vector3 _southEast = new Vector3(1, -1, 0);
    private Vector3 _southWest = new Vector3(-1, -1, 0);
    private Vector3 _northWest = new Vector3(-1, 1, 0);

    private bool _moving = true;
    
    private int _ufoShield = 5;

    private UIManager _uiManager;
    private GameManager _gameManager;

    [SerializeField]
    private GameObject _clusterBomb;
    [SerializeField]
    private GameObject _explosionPrefab;

    void Start()
    {
        _player = GameObject.Find("Player_2D").GetComponent<Player2D>();
        if (_player == null)
            Debug.LogError("UFO- Player not found");

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("UIManager Not Located(SpawnManager)");
        }
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("SpawnManager.cs- Unable to Locate Game Manager");
        }



        _northEast = Vector3.Normalize(_northEast);
        _southEast = Vector3.Normalize(_southEast);
        _southWest = Vector3.Normalize(_southWest);
        _northWest = Vector3.Normalize(_northWest);
    }

    void Update()
    {
        UFOMovement();
    }

    void UFOMovement()
    {
        if (transform.position.x > 8.5f)
            transform.position = new Vector3(-8.5f, transform.position.y, 0);
        if (transform.position.x < -8.5f)
            transform.position = new Vector3(8.5f, transform.position.y, 0);
        if (transform.position.y > 6)
            transform.position = new Vector3(transform.position.x, -6, 0);
        if (transform.position.y < -6)
            transform.position = new Vector3(transform.position.x, 6, 0);

        switch(_movementDir)
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

        randomTime = Random.Range(1, _maxTime);
        if (_moving == true)
        {
            StartCoroutine(MovementDuration());
        }        
    }

    IEnumerator MovementDuration()
    {
        _moving = false;
        _movementDir = Random.Range(0, 8);
        yield return new WaitForSeconds(randomTime);
        Instantiate(_clusterBomb, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(.1f);
        _moving = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _player.TakeDamage();
        }
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            UFOTakeDamage();
        }
    }

    private void UFOTakeDamage()
    {
        _ufoShield--;
        if (_ufoShield < 1)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject, .1f);
            UFODeath();
        }
    }

    private void UFODeath()
    {
        _gameManager.GameOver();
        _uiManager.GameOverSequence(2);
        //PlayerDeath();
    }

}
