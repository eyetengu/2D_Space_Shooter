using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveLeft : MonoBehaviour
{
    private float _speed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
        Destroy(this.gameObject, 4f);
    }
}
