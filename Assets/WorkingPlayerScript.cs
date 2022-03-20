using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingPlayerScript : MonoBehaviour
{
    [SerializeField]
    private CharacterController _player;

    [SerializeField]
    private float _gravity = -9.81f;

    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _speedBoost = 1f;
    [SerializeField]
    private float _speedometer;

    [SerializeField]
    float _sensitivity = 1;

    [SerializeField]
    private bool _isRunning;

    void Start()
    {
        _player = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        CalculatePlayerMovement();
    }

    private void CalculatePlayerMovement()
    {
       float horizontalInput = Input.GetAxis("Horizontal");
       float verticalInput = Input.GetAxis("Vertical");

       float _mouseX = Input.GetAxis("Mouse X");

        _speedometer = _speed * _speedBoost;


       Vector3 direction = new Vector3(0, 0, verticalInput);
       Vector3 velocity = direction * _speed * _speedBoost * Time.deltaTime;
       Quaternion rotation = Quaternion.LookRotation(velocity, direction);
       transform.rotation = rotation;
        
        // Vector3 newRotation = transform.localEulerAngles;
        // newRotation.y += _mouseX * _sensitivity;
        // transform.localEulerAngles = newRotation;




        if(Input.GetKey(KeyCode.LeftShift))
        {
                _isRunning = true;
                _speedBoost = 2.0f;
                _player.Move(velocity);
                Debug.Log(_speedometer);

        }
        else
        {
                _isRunning = false;
                _speedBoost = 1.0f;
                _player.Move(velocity);
                Debug.Log(_speedometer);
        }
    }
}
