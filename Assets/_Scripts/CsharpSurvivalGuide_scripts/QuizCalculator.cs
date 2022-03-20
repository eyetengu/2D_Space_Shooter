using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizCalculator : MonoBehaviour
{
    //5 randomly produced grades
    //find quiz grade average
    public float quiz1, quiz2, quiz3, quiz4, quiz5;
    private float quizAverage;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            QuizGrades();
        }
    }

    void QuizGrades()
    {
        quiz1 = Random.Range(50f, 1000f);
        quiz2 = Random.Range(40f, 100f);
        quiz3 = Random.Range(40f, 100f);
        quiz4 = Random.Range(80f, 100f);
        quiz5 = Random.Range(30f, 100f);

        quizAverage = (quiz1 + quiz2 + quiz3 + quiz4 + quiz5) / 5;
        quizAverage = Mathf.Round(quizAverage * 100) / 100;
        Debug.Log("Quiz Average: " + quizAverage);

    }
}
