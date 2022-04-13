using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2D_FiringType : Enemy2D
{
    //This is an enemy script of type: Firing-Laser
    [SerializeField]
    private GameObject _enemyLaserSingle;

    private bool _canFire = true;
    [SerializeField]
    private bool _canDestroyPowerups;

    private GameObject[] _powerUps;

    [SerializeField]
    private float _sensorRadius = 3f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(_canDestroyPowerups == true && _canFire == true)
        {
            _powerUps = GameObject.FindGameObjectsWithTag("PowerUp");
            foreach(GameObject powerUP in _powerUps)
            {
                float distance = Vector3.Distance(powerUP.transform.position, this.transform.position);
                if(distance <= _sensorRadius)
                {
                    Debug.Log("powerup in the vicinity");
                    if(powerUP.transform.position.y < this.transform.position.y -1.5f)
                    {
                        Debug.Log("powerup nearing target lock zone");
                        if(powerUP.transform.position.x > transform.position.x - .5f &&
                            powerUP.transform.position.x < transform.position.x + .5f)
                        {
                            StartCoroutine(EnemyFireRate());
                        }
                    }
                }

            }
        }
        else if(_canDestroyPowerups == false && _canFire == true)
        {
            StartCoroutine(EnemyFireRate());
        }


        if(Input.GetKeyDown(KeyCode.J) && _canFire == true)
        {
            StartCoroutine(EnemyFireRate());
        }
    }

    IEnumerator EnemyFireRate()
    {
        _canFire = false;
        Instantiate(_enemyLaserSingle, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        _canFire = true;
    }







}
