using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTripleBlast : MonoBehaviour
{
    private Vector3 direction;
    private Vector3 velocity;

    public int _speed = 3;
    private int _duration = 0;   
    public float _delay1 = 1.0f;

    [SerializeField]
    private bool _diamondDown;
    public bool _check1;
    public bool _check2;

    void Start()
    {
    }

    void Update()
    {
        //PositionCheck
        if(transform.position.y < -6)
        {
            transform.position = new Vector3(Random.Range(-7, 8), 8, 0);
        }
        if (transform.position.x < -11)
        { 
            transform.position = new Vector3(11,Random.Range(-5, 5), 0); }
        
        //DiamondPattern
        if(_check1 == false && _check2 == false)
        {
            direction = new Vector3(-1, -1, 0);
            velocity = direction * _speed * Time.deltaTime;
            transform.Translate(velocity);
            StartCoroutine(DiamondDirection1());
        }

        else if(_check1 == true && _check2 == false)
        {
            direction = new Vector3(1, -1, 0);
            velocity = direction * _speed * Time.deltaTime;
            transform.Translate(velocity);

            StartCoroutine(DiamondDirection2());
        }

        else if (_check1 == true && _check2 == true)
        {
            direction = new Vector3(1, 1, 0);
            velocity = direction * _speed * Time.deltaTime;
            transform.Translate(velocity);

            StartCoroutine(DiamondDirection3());
        }
        else if (_check1 == false && _check2 == true)
        {
            direction = new Vector3(-1, 1, 0);
            velocity = direction * _speed * Time.deltaTime;
            transform.Translate(velocity);

            StartCoroutine(DiamondDirection4());
        }

    }

    IEnumerator DiamondDirection1()
    {
        yield return new WaitForSeconds(_delay1);
        _check1 = true;
        _check2 = false;
    }

    IEnumerator DiamondDirection2()
    {
        yield return new WaitForSeconds(_delay1);
        _check1 = true;
        _check2 = true;
    }
    IEnumerator DiamondDirection3()
    {
        yield return new WaitForSeconds(_delay1);
        _check1 = false;
        _check2 = true;
    }
    IEnumerator DiamondDirection4()
    {
        yield return new WaitForSeconds(_delay1);
        _check1 = false;
        _check2 = false;
    }

    
}