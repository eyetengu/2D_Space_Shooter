using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AvoidanceScript : MonoBehaviour
{
    //goal to make an enemy behavior that moves out of the path of the players laser
    //we will create a 'zone' of influence' that will signal the enemy to move
    [SerializeField]
    private GameObject[] _lasers;
    [SerializeField]
    private float _dodgeSpeed = 2.0f;
   
    void Update()
    {
        _lasers = GameObject.FindGameObjectsWithTag("Laser");

        foreach (GameObject laser in _lasers)
        {
            float distance = Vector3.Distance(laser.transform.position, this.transform.position);
            float laserX = laser.transform.position.x;

            if(distance < 3.0f && (laser.transform.position.y < this.transform.position.y))
            { 
                if(laserX > this.transform.position.x)
                {
                    transform.Translate(Vector3.left * _dodgeSpeed * Time.deltaTime);//this.transform.position.x - 1.0f;
                }
                if(laserX < this.transform.position.x)
                {
                    transform.Translate(Vector3.right * _dodgeSpeed * Time.deltaTime);//this.transform.position.x + 1.0f;
                }
                if(laserX == this.transform.position.x)
                {
                    transform.Translate(Vector3.right * _dodgeSpeed * Time.deltaTime);
                }
            }
        }
    }
}
