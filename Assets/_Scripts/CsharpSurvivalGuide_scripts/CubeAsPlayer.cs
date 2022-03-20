using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAsPlayer : MonoBehaviour
{
    public GameObject[] cubes;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            foreach(var cube in cubes)
            {
                GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
    }
}
