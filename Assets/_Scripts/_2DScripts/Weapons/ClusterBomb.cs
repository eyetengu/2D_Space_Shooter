using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterBomb : MonoBehaviour
{
    private Player2D _player;
    [SerializeField]
    private int _bombShellID;

    private float _speed = 2f;

    private Vector3 _northEast = new Vector3( 1, 1, 0);
    private Vector3 _southEast = new Vector3( 1, -1, 0);
    private Vector3 _southWest = new Vector3( -1, -1, 0);
    private Vector3 _northWest = new Vector3( -1, 1, 0);


    void Start()
    {
        _player = GameObject.Find("Player_2D").GetComponent<Player2D>();
        _northEast = Vector3.Normalize(_northEast);
        _southEast = Vector3.Normalize(_southEast);
        _southWest = Vector3.Normalize(_southWest);
        _northWest = Vector3.Normalize(_northWest);
    }

    // Update is called once per frame
    void Update()
    {
        switch(_bombShellID)
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
                Debug.LogError("Nice Try_ try another bombshell");
                break;
        }

        Destroy(this.gameObject, 2.5f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _player.TakeDamage();
            Destroy(this.gameObject);
        }
    }

}
