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
            if(_sendDrones == true)
            {
                StartCoroutine(CountdownToDrones());
                if(_explosiveRoundsActive == true)
                {
                    StartCoroutine(ExplosiveRounds());
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

        if(transform.position.y >= 11f)
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
        for(int i = 0; i < _bombshell.Length; i++)
        {
            Debug.Log("Explosive shell sent in " + _compassDirection);//send out an explosiveShell in direction 0, 45, 90, 135, 180,225, 270, 315
            Instantiate(_bombshell[i], _droneSpawnTransform.position, Quaternion.identity);

            //wait for .75 seconds
            yield return new WaitForSeconds(3f);
            _compassDirection += 45;
            if(_compassDirection > 315)
            {
            }
        }

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
