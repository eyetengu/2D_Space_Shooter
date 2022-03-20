using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginnerPlayerCube : MonoBehaviour
{
    public int weaponID;

    void Update()
    {
        switch(weaponID)
        {
            case 1:
                Debug.Log("You have Chosen the GUN");
                break;
            case 2:
                Debug.Log("You have Chosed the Knife");
                break;
            case 3:
                Debug.Log("You have Chosen the Machine Gun");
                break;
            default: 
                Debug.Log("Please Make A Selection");
                break;
        }
    }
}