using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eel : MonoBehaviour
{
    [SerializeField]
    public float _detectionRadius = 5f; 
    private GameObject _player;
    private float _speed = 1;
    private float _speedMultiplier = 1;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        Debug.Log(_player.name);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(_player.transform.position, this.transform.position);
        Debug.Log(distance + " " + _detectionRadius);
        if(distance < _detectionRadius)
        {
            Debug.Log("Q-Bert MUST DIE");
        }





    }
}
