using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponSystem : MonoBehaviour
{
    private float _speed = 3f;
    public int directionID;
    // Start is called before the first frame update
    void Start()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ProjectileDirection()
    {
       
    }

}
