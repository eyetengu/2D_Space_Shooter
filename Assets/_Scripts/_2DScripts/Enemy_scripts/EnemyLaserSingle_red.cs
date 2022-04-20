using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserSingle_red : MonoBehaviour
{
    private float _speed = 7f;
    private Player2D _player2d;


    void Start()
    {
        _player2d = GameObject.Find("Player_2D").GetComponent<Player2D>();
    }


    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        Destroy(this.gameObject, 2f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(this.gameObject);
        if (other.tag == "Player2D")
        {
            _player2d.TakeDamage();
        }
    }
}
