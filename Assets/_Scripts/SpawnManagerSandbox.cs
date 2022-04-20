using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerSandbox : MonoBehaviour
{
    [SerializeField]
    private bool _spawning = true;


    [SerializeField]
    private bool _basicSpawn = false;
    [SerializeField]
    private bool _basicSpawnedAlready;
    [SerializeField]
    private float _basicPass = 0f;
    //    [SerializeField]
    //  private float _basicTotalCount = 0f;
    [SerializeField]
    private float _basicCountAverage = 0f;
    [SerializeField]
    private float[] _basicCounts;
    [SerializeField]
    private Transform[] _basicPowerupTransforms;
    //private int _basicDelay = 5;


    [SerializeField]
    private bool _rareSpawn = false;
    //private int _rareDelay = 10;
    //[SerializeField]
    //public Transform _rarePowerupPrefab;
    [SerializeField]
    private Transform[] _rarePowerupTransform;

    [SerializeField]
    private bool _epicSpawn = true;
    //private int _epicDelay = 2;
    [SerializeField]
    private Transform[] _epicPowerupTransform;

    private int personalCount;
    private int average;
    //public int[] epicCount;

    void Start()
    {

    }

    void Update()
    {
        //calculate the average 

        if (_spawning)
        {
            if (_basicSpawn)
            {
                StartCoroutine(BasicPowerUpSpawn());
            }
            if (_rareSpawn)
            {
                StartCoroutine(RarePowerUpSpawn());
            }
            if (_epicSpawn)
            {
                foreach (Transform epicPowerup in _epicPowerupTransform)
                {
                    //epicCount[current]++;
                    //if(personalCount <= average)
                    {
                        //_epicSpawn = false;
                        //personal int ++
                        //StartCoroutine(EpicPowerUpSpawn());
                    }
                }
            }
        }
        else
        {
            Debug.Log("GameOverSequence");
        }
    }

    IEnumerator BasicPowerUpSpawn()
    {
        Debug.Log("Coroutine entered");
        //immediately disable entry into this coroutine until finished
        _basicSpawn = false;
        _basicPass++;
        
        if (_basicPass > 3)
        { _basicPass = 1; }
        
        _basicCountAverage = _basicPass / _basicPowerupTransforms.Length;


        if (_basicCountAverage == 0)
        { _basicPass = 0; } //
        //_basicTotalCount = 0; }


        for (int i = 0; i < _basicPowerupTransforms.Length; i++)
        {
            Debug.Log("For Loop Entered");
            //Debug.Log("powerup.length " + _basicPowerupTransforms.Length);
            if (_basicSpawnedAlready == false)
            {
                if (_basicCounts[i] <= _basicCountAverage)                                //_basicCount[current]
                {
                    Vector3 posToSpawnBasic = new Vector3(Random.Range(-8, 9), 7, 0);
                    Instantiate(_basicPowerupTransforms[i], transform.position, Quaternion.identity);             //basicPowerup[current]
                    _basicCounts[i]++;
                    if (_basicCounts[i] > 2)
                        _basicCounts[i] = 0;
                    _basicSpawnedAlready = true;
                }
            }
        }
        //Debug.Log(_basicCounts[0] + " " + _basicCounts[1] + " " + _basicCounts[2]);
        yield return new WaitForSeconds(5);

        StartCoroutine(BufferTimer());
    }

    IEnumerator BufferTimer()
    {
        Debug.Log("Coroutine #2 Entered");
        _basicSpawn = true;
        _basicSpawnedAlready = false;

        yield return new WaitForSeconds(1f);
        
    }


    IEnumerator RarePowerUpSpawn()
    {
        //Vector3 posToSpawnRare = new Vector3(Random.Range(-8, 8), 8, 0);
        //_rareSpawn = false;
        yield return new WaitForSeconds(10);
        //Instantiate(_rarePowerupPrefab, posToSpawnRare, Quaternion.identity);
        //_rareSpawn = true;
    }

    IEnumerator EpicPowerUpSpawn()
    {
        //Vector3 posToSpawnEpic = new Vector3(Random.Range(-8, 9), 7.5f, 0);
        //_epicSpawn = false;
        //int randomEpic = Random.Range(0, _epicPowerupTransform.Length);

        yield return new WaitForSeconds(15);

        //Instantiate(_epicPowerupPrefab[randomEpic], posToSpawnEpic, Quaternion.identity);
        //_epicSpawn = true;
    }

    
}
