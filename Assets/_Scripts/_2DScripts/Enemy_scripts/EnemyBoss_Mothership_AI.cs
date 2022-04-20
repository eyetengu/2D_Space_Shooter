using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss_Mothership_AI : MonoBehaviour
{
    [SerializeField]
    private float _travelSpeed = 1.0f;
    [SerializeField]
    private bool _canTravel = true;

    [SerializeField]
    private GameObject _drone;
    [SerializeField]
    private Transform _droneSpawnTransform;
    private bool _sendDrones = true;

    [SerializeField]
    private bool _timeToGo = false;

    private int _compassDirection = 0;
    private bool _explosiveRoundsActive = true;
    [SerializeField]
    private GameObject[] _bombshell;

    [SerializeField]
    private GameObject _shockWavePlus;
    [SerializeField]
    private GameObject _shockWaveX;

    void Start()
    {
        transform.position = new Vector3(0, 11, 0);
        _droneSpawnTransform = GameObject.Find("DroneSpawnTransform").GetComponent<Transform>() ;
    }

    void Update()
    {
        if(_canTravel == true)
        {
            MotherShipStage();
        }
        if (_timeToGo == true)
        { 
            UFODepart();
        }
    }

    void MotherShipStage()
    {
        if(_canTravel == true && transform.position.y > 0)
        {
            transform.Translate(Vector3.down * _travelSpeed * 2 * Time.deltaTime);
        }
        else
        {
            _canTravel = false;
            StartCoroutine(ExplosiveRounds());
            if(_sendDrones == true)
            {
                StartCoroutine(CountdownToDrones());
                if(_explosiveRoundsActive == true)
                {
                }
            }
        }
    }

    public void UFODepart()
    {
        //_timeToGo = true;
        //transform.position = new Vector3(0, 0, 0);
        Debug.Log("Im outta here");

        transform.Translate(Vector3.up * _travelSpeed * 3 * Time.deltaTime);

        if(transform.position.y >= 12f)
        {
            _timeToGo = false;
        }        
    }

    void SendOutTheDrones()
    {
        _sendDrones = false;
        
        Instantiate(_drone, _droneSpawnTransform.position, Quaternion.identity);
        _canTravel = false;
        _timeToGo = true;
    }

    IEnumerator CountdownToDrones()
    {
        yield return new WaitForSeconds(5);
        if(_sendDrones == true)
        {
            SendOutTheDrones();
        }
    }

    IEnumerator ExplosiveRounds()
    {
        _explosiveRoundsActive = false;
        
            Instantiate(_shockWavePlus, transform.position, Quaternion.identity);


            yield return new WaitForSeconds(1f);
            Instantiate(_shockWaveX, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(1f);
            Instantiate(_shockWavePlus, transform.position, Quaternion.identity);

        

        //launch several explosive rounds. they act as flak and cause damage on explosion
        //each round will have a blast radius that can damage the player
        // rounds come out pretty quickly and blast at a random.range distance from their origin
        //overall effect to last about 8 seconds
    }

    private void QuickFire()
    {
        //enemy moves back and forth quickly and shoots in 3-shot bursts
        // lasts about 7 seconds.

    }

    private void MotherShipDamageControl()
    {
        //when mothership is vulnerable and is shot by the player it loses a point of damage
        //when it sustains 5 points of damage then it is dead and an animation sequence will play
        //a message will be sent to the UI declaring the player as winner.
    }


    
}
