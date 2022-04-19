using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public GameObject enemyTarget;

    public float rotateTowardTargetSpeed = 1.0f;
    public GameObject _explosionPrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyTarget = GameObject.FindWithTag("Enemy");

        BasicHomingMovement();
        Destroy(this.gameObject, 4f);
    }

    void BasicHomingMovement()
    {
        Vector3 directionToTarget = enemyTarget.transform.position - transform.position;
        float rotationValue = rotateTowardTargetSpeed * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, directionToTarget, rotationValue, 1f);
        Debug.DrawRay(transform.position, directionToTarget, Color.blue);

        transform.rotation = Quaternion.LookRotation(newDirection);
        transform.position = Vector3.MoveTowards(this.transform.position, enemyTarget.transform.position, .1f);
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
