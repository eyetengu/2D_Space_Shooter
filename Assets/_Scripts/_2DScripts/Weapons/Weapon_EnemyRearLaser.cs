using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_EnemyRearLaser : MonoBehaviour
{
    [SerializeField]
    private Player2D _player;
    private Animator _gameCameraAnimator;

    [SerializeField]
    private GameObject _explosionPrefab;

    private float _speed = 2.5f;

    void Start()
    {
        _player = GameObject.Find("Player_2D").GetComponent<Player2D>();
        _gameCameraAnimator = GameObject.Find("Main Camera").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        Destroy(this.gameObject, 2f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            _player.TakeDamage();
            _gameCameraAnimator.SetTrigger("CameraShake_bool");
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if(other.gameObject.tag == "EnemyLaser")
        {

        }
    }


}
