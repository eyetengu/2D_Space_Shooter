using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAppearance : MonoBehaviour
{
    [Range (0f, 30f)]
    public float mySlider;

    void Start()
    {
        transform.position = new Vector3(Random.Range(-10, 10), Random.Range(0, 10), Random.Range(-10, 0));
    }

    // Update is called once per frame
    void Update()
    {
        float newSlider = mySlider;
        transform.Translate(Vector3.forward * newSlider * Time.deltaTime);
        Debug.Log("Speed: " + mySlider);
    }
}
