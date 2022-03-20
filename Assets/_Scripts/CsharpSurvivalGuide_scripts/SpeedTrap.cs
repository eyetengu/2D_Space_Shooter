using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTrap : MonoBehaviour
{
    public int speed;
    public int maxSpeed;

    void Start()
    {
        maxSpeed = Random.Range(60,121);
        speed = 0;
        StartCoroutine(SpeedZone());
    }


    void Update()
    {
    }

    IEnumerator SpeedZone()
    {
        while(speed < maxSpeed)
        {
            yield return new WaitForSeconds(1.0f);
            speed += 5;
        }
        Debug.Log("SCREEEEEEEEECH!");
        
    }





}
