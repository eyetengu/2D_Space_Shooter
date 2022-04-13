using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2D_QuadBlaster : MonoBehaviour
{
    public Transform _weaponQuadBlaster;
    public bool _isReady;

    // Start is called before the first frame update
    void Start()
    {
        _isReady = true;
    }

    void Update()
    {
        if( _isReady == true)
        {
            _isReady = false;
            StartCoroutine(QuadBlasterCoolDown());
        }
        if(Input.GetKey(KeyCode.N))
        {
            QuadInstantiate();
        }
    }

    // Update is called once per frame
    IEnumerator QuadBlasterCoolDown()
    {       
        Instantiate(_weaponQuadBlaster, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(3f);
        _isReady = true;
        
    }

    public void QuadInstantiate()
    {
        Instantiate(_weaponQuadBlaster, transform.position, Quaternion.identity);

    }
}
