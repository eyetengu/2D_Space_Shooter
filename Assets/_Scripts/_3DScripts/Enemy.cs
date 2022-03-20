using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    [SerializeField]
    private Player _player;
    [SerializeField]
    //private Transform _explosion;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        transform.position = new Vector3(Random.Range(-9, 9), 6, 0);

        if(_player == null)
        {
            Debug.Log("Player Script Not Found");
        }

    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    void EnemyMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y <= -7)
        {
            float randomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomX, 6, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                player.TakeDamage();
            }

            Destroy(this.gameObject);
        }
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            _player.UpdateScore();
            Destroy(this.gameObject);
        }  

    }


}
