using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //add an _isSteerable option to add a manual heatseek option.
    //think manned torpedos or the small craft that are part of larger craft
    //serenity has two pods, the enterprise has at least one small transport vessel, etc.

    public enum Directions { up, back, left, right, seek }
    public Directions direction = Directions.up;
    [SerializeField]
    private float _projectileSpeed = 3f;
    [SerializeField]
    private Transform _enemy;

    void Start()
    {
        //transform.Translate(Vector3.direction * _projectileSpeed * Time.deltaTime);
    }

void Update()
    {
        MainFunction();
        Debug.Log("Update");
    }


    void MainFunction()
    {
        Debug.Log("Direction " + direction);
        if (direction == Directions.up)
        {
            ProjectileUp();
        }
        else if (direction == Directions.back)
        {
            ProjectileBack();
        }
        else if (direction == Directions.left)
        {
            ProjectileLeft();
        }
        else if (direction == Directions.right)
        {
            ProjectileRight();
        }
        else if (direction == Directions.seek)
        {
            ProjectileSeek();
        }

    }

    void ProjectileUp()
    {
        transform.Translate(Vector3.up * _projectileSpeed * Time.deltaTime);
    }
    void ProjectileBack()
    {
        transform.Translate(Vector3.down * _projectileSpeed * Time.deltaTime);
    }
    void ProjectileLeft()
    {
        transform.Translate(Vector3.left * _projectileSpeed * Time.deltaTime);
    }
    void ProjectileRight()
    {
        transform.Translate(Vector3.right * _projectileSpeed * Time.deltaTime);
    }
    void ProjectileSeek()
    {
        Vector3 targetDirection = _enemy.position - transform.position;
        float singleStep = _projectileSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0f);
        Debug.DrawRay(transform.position, newDirection, Color.red);
        transform.rotation = Quaternion.LookRotation(newDirection);
        transform.Translate(Vector3.forward * _projectileSpeed * Time.deltaTime);
        //Debug.Log("Projectile Seek");
        //GameObject[] enemies;
        //enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //foreach(GameObject enemy in enemies)
        //{
            //float distance = Vector3.Distance(enemy.transform.position, this.transform.position);
            //float direction = Vector3.Direction(enemy.transform.position, this.transform.position);
        //}
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision Detected");
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        //entry damage
    }

    void OnTrigger()
    {
        //poison damage, burn damage(chemical, fire, liquid)
    }

    void OnTriggerExit()
    {
        //exit wound (bullet, arrow, radiation)
    }
}
