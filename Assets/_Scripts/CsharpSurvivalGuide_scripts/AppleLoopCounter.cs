using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleLoopCounter : MonoBehaviour
{
    public int apples;
    public bool allCollected = false;

    void Start()
    {
        StartCoroutine(AppleUpdate());
    }

    IEnumerator AppleUpdate()
    {
        
        for(int i = 0; i < 25; i ++)
        {
            apples ++;
            yield return new WaitForSeconds(1.0f);
        Debug.Log("Apple Count Increased by 1");
        }
        Debug.Log("Badee Badee Badee... That's All Folks!");
        allCollected = true;
    }
}
