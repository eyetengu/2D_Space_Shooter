using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_EnemyLaser : MonoBehaviour
{
    private float _speed = .50f;
    private bool _canFire = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime);
        Destroy(this.gameObject, 2f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "PowerUp")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject, .2f);
        }
    }
}
