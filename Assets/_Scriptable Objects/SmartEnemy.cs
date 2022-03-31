using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SmartEnemy.asset", menuName = "EnemyInventory/Enemy/SmartEnemy")]
public class SmartEnemy : EnemySO
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
        //float distance = Vector3.Distance(_player.transform.position, this);
        //Debug.Log(distance + " " + _detectionRadius);
        //if(distance < _detectionRadius)
        //{
            //Debug.Log("Q-Bert MUST DIE");
        //}


        // this particular enemy unit will look for the player GameObject
        // Next, it will use its detection radius to determine whether or not the player is within in range.
        //if the player is within range then the enemy will move, slowly at first, towards the player with a _maxSpeed
        //upon collision this unit will be destroyed.



    }
    
}
