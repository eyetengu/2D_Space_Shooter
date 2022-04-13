using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyPickup : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _powerups;

    [SerializeField]
    private GameObject _enemyLaser2D;

    [SerializeField]
    private bool _canFire = true;

    void Start()
    {
        if (_powerups == null)
            Debug.Log("Powerups? We don't need no stinking powerups!");

        //StartCoroutine(FireUponPickup());
        Debug.Log("Dees ees dee Ess Ess Senterprice! Yield to Our Might");
    }

    void Update()
    {
        Debug.Log("Resistance is Futile! Yield und Obey!");
        _powerups = GameObject.FindGameObjectsWithTag("PowerUp");
        foreach (GameObject powerup in _powerups)
        {
            Debug.Log("Power up up and away" + powerup.name);
            float distance = Vector3.Distance(powerup.transform.position, this.transform.position);
            if (distance <= 3f && powerup.transform.position.y < (this.transform.position.y -1f))
            {
                Debug.Log("Target is almost in range");
                if(powerup.transform.position.x <= (this.transform.position.x + .5f) 
                    && powerup.transform.position.x >= (this.transform.position.x + -.5f))
                {
                    if(_canFire == true)
                    {
                        Debug.Log("Target Locked");
                        StartCoroutine(FireUponPickup());
                    }
                }
            }
        }
    }

    IEnumerator FireUponPickup()
    {
        _canFire = false;

        Instantiate(_enemyLaser2D, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(.65f);
        _canFire = true;
    }
}
