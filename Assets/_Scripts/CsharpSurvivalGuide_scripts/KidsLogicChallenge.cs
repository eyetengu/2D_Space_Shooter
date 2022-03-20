using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidsLogicChallenge : MonoBehaviour
{
    public string[] names = new string[] {"Eric", "Chris", "Brian", "Laney", "Ava"};
    public int[] ages = new int[] {51, 50, 47, 12, 8};
    public string[] cars = new string[] {"Ford", "Fiero", "Ferrari", "Fiat", "Fugo"};
    
    public int random;

    void Start()
    {
        Debug.Log("Begin!");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("UPDATE");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for(int i = 0; i < names.Length; i ++)
            {
                Debug.Log(names[i] + "  " + ages[i] + "  " + cars[i]);
            }



            //random = Random.Range(0, names.Length);
            //Debug.Log(names[random] + "  " + ages[random] + "  " + cars[random]);
        }
    }
}
