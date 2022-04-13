using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_ZigZag : MonoBehaviour
{
    private Vector3 direction;
    private Vector3 velocity;

    public float _speed = 1;
    public float _delay = 1;
    private int _duration = 0;

    [SerializeField]
    private bool _zigZag;
    private bool _zig;
    private bool _zag;

   


    void Update()
    {
        if (transform.position.y < -6)
        {
            transform.position = new Vector3(Random.Range(-7, 8), 7, 0);
        }

        if (_zigZag == true)
        { ZigZag(); }


    //ZigZag Functions
    void ZigZag()
    {
        if (_zig == false)
        {
            direction = new Vector3(-1, -1, 0);
            velocity = direction * _speed * Time.deltaTime;
            transform.Translate(velocity);
            StartCoroutine(ZigSwitch());
        }
        else if (_zig == true)
        {
            direction = new Vector3(1, -1, 0);
            velocity = direction * _speed * Time.deltaTime;
            transform.Translate(velocity);

            StartCoroutine(ZigSwitchBack());
        }

    }

    IEnumerator ZigSwitch()
    {
        yield return new WaitForSeconds(_delay);
        _zig = true;
    }

    IEnumerator ZigSwitchBack()
    {
        yield return new WaitForSeconds(_delay);
        _zig = false;
    }

}
}
