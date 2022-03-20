using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberIncrementer : MonoBehaviour
{
    public int counter = 0;
    
    void Start()
    {
        StartCoroutine(CounterUpdate());
    }

    IEnumerator CounterUpdate()
    {
            Debug.Log("Hola! Como Estas?");
        while(counter < 31)
        {
            yield return new WaitForSeconds(1.0f);
            if(counter >0 && counter < 11)
            {
                Debug.Log(counter);
            }

            else if(counter > 10 && counter <21)
            {
                if((counter % 2) == 0)
                {
                    Debug.Log(counter);
                }
            }

            else if(counter >20 && counter < 30)
            {
                if((counter % 2) == 1)
                {
                Debug.Log("Hey, Cutie");
                    Debug.Log(counter);
                }
            }
            else if(counter == 30)
            {
                Debug.Log("Whew! Im Beat");
            }
            counter ++;
        }
        Debug.Log("Muy Bien! Y tu?");
    }
}

//while i is less than 31

//if (i % 10)
    //print value of i