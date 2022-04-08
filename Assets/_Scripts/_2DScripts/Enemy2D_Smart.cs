using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2D_Smart : MonoBehaviour
{

    [SerializeField]
    private Transform target;
    private float speed = 2f;

    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, step);                
    }
}
