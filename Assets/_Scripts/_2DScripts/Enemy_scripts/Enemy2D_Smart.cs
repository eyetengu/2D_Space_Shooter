using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2D_Smart : MonoBehaviour
{
    [SerializeField]
    private Player2D _player2D;
    [SerializeField]
    private Transform _playerTarget;
    [SerializeField]
    private GameObject _rearFire;

    [SerializeField]
    private BoxCollider2D _boxCollider;

    private bool _canFire = true;

    void Start()
    {
        _player2D = GameObject.Find("Player_2D").GetComponent<Player2D>();
        _playerTarget = GameObject.Find("Player_2D").GetComponent<Transform>();
    }

    void Update()
    {
        if(_playerTarget != null)
        {
            if(_playerTarget.position.y > this.transform.position.y && _canFire == true)
            {
                _canFire = false;
                StartCoroutine(RearFire());
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player_2D")
        {
            _boxCollider.enabled = false;
            _player2D.TakeDamage();
            Destroy(this.gameObject);
        }
    }

    IEnumerator RearFire()
    { 
        Instantiate(_rearFire, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(.45f);
        _canFire = true;
    }
}
