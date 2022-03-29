using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //I am the Wave Manager... Hear me purrr... like a Bugatti!

    //As the wave manager I am responsible for communicating
    //with theSpawnManager and I have to know about the 
    //various enemies and powerups that I have at my disposal

    //I need to have the ability to create custom waves by determining various factors:

    //the first is to know how many sub-waves or rounds will be in each of the waves.
    //I will also require access to the types of enemies/powerups to use so that I can assign
    //them to the appropriate sub-waves.
    //As well I will need to assign a number of each of them to spawn
    //I will keep track of how many of each enemy/powerup and total enemy/powerup count present in the scene
    //I will be responsible for maintaining a healthy spawn rate for enemies/powerups and ensure that no more are present than is necessary




    int[] waves;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
