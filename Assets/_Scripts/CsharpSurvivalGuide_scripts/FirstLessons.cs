using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLessons : MonoBehaviour
{
    public int quiz1, quiz2, quiz3, quiz4, quiz5;
    public int averageScore;
    public string letterGrade;

    void Start()
    {
    }


    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            RandomTestAverage();
        }

    }

    void RandomTestAverage()
    {
            quiz1 = Random.Range(40, 100);
            quiz2 = Random.Range(40, 100);
            quiz3 = Random.Range(40, 100);
            quiz4 = Random.Range(40, 100);
            quiz5 = Random.Range(40, 100);

            averageScore = (quiz1 + quiz2 + quiz3 + quiz4 + quiz5) / 5;

            if(averageScore >= 90)
            {
                letterGrade = "A";
            }

            else if(averageScore >= 80)
            {
                letterGrade = "B";
            }

            else if(averageScore >= 70)
            { 
                letterGrade = "C";
            }

            else
            {
                letterGrade = "F";
            }

            Debug.Log("The Average Score was " + averageScore + "% which equivalent to a/n " + letterGrade + " average.");

    }

}
