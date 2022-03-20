using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCubeColor : MonoBehaviour
{
    public int health = 100;
    public bool isDead = false;


    void Start()
    {
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isDead == false)
        {
            health -= Random.Range(0,6);
            if(health <= 0)
            {
                health = 0;
                isDead = true;
                DeadDeadDeadDead();
            }
        }
        //is player dead

    }

    void DeadDeadDeadDead()
    {
        Debug.Log("Player had Died Died Died Died!");
    }

}