using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTestDesign : MonoBehaviour
{
    public CircleCollider2D _collider2d;
    // Start is called before the first frame update
    void Start()
    {
        _collider2d = GetComponent<CircleCollider2D>();
        if( _collider2d == null )
        {
            Debug.Log("No CircleCollider2D found");
            _collider2d.radius = 3f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
