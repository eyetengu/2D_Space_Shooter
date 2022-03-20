using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser2D : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
        
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        
        if(transform.position.y >= 8.0f)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
