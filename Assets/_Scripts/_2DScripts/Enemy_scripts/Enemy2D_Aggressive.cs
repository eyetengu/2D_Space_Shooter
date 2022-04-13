using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2D_Aggressive : Enemy2D
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float speed = 3f;

    void Start()
    {
        target = GameObject.Find("Player_2D").GetComponent<Transform>();
        if(target == null)
        {
            Debug.LogError("Enemy Aggressive- Player not found");
        }
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, step);
    }
}
