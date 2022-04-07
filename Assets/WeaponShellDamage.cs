using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShellDamage : MonoBehaviour
{
    private Player2D _player;
    [SerializeField]
    private GameObject _explosionPrefab;
    
    void Start()
    {
        _player = GameObject.Find("Player_2D").GetComponent<Player2D>();
        if(_player == null)
        {
            Debug.LogError("WeaponShellDamage- no player script");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _player.TakeDamage();
        }
    }
}
