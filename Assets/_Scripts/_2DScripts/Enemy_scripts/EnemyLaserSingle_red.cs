using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserSingle_red : MonoBehaviour
{
    private float _speed = 7f;

    void Start()
    {
        
    }


    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        Destroy(this.gameObject, 2f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Destroy(this.gameObject);
    }
}
