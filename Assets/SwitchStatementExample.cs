using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchStatementExample : MonoBehaviour
{
    private int _counter = 0;
    private bool _ready = true;
    
    void Update()
    {
        if(_ready)
        {
            switch(_counter)
            {
                case 0:
                    _ready = false;
                    Debug.Log(_counter + " Roses Are Red");
                    StartCoroutine(Timer());
                    break;
                case 1:
                    _ready = false;
                    Debug.Log(_counter + " Violets Are Blue");
                    StartCoroutine(Timer());
                    break;
                case 2:
                    _ready = false;
                    Debug.Log(_counter + " It Turns Out The Sasquatch");
                    StartCoroutine(Timer());
                    break;
                default:
                    _ready = false;
                    Debug.Log(_counter + " Is Deeply In Love With You");
                    _ready = false;
                    break;
            }
        }
    }

    IEnumerator Timer()
    {
        _counter++;
        yield return new WaitForSeconds(2);
        _ready = true;
    }
}
