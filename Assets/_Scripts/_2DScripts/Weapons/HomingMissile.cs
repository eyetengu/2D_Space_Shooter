using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public GameObject enemyTarget;
    public GameObject _explosionPrefab;

    public float rotateTowardTargetSpeed = 1.0f;
    private bool _isReady = true;


    void Update()
    {
        if(_isReady == true)
        {
            enemyTarget = GameObject.FindWithTag("Enemy");

            BasicHomingMovement();
            Destroy(this.gameObject, 4f);
        }
    }

    void BasicHomingMovement()
    {
        if(enemyTarget != null)
        {
            Vector3 directionToTarget = enemyTarget.transform.position - transform.position;
            //float rotationValue = rotateTowardTargetSpeed * Time.deltaTime;

            //Vector3 newDirection = Vector3.RotateTowards(transform.forward, directionToTarget, rotationValue, 1f);
            Debug.DrawRay(transform.position, directionToTarget, Color.blue);

            //transform.rotation = Quaternion.LookRotation(newDirection);
            transform.position = Vector3.MoveTowards(this.transform.position, enemyTarget.transform.position, .01f);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Homing Trigger Activated");
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject, .2f);
        }
    }
}
