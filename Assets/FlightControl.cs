using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightControl : MonoBehaviour
{
    public int degrees = 0;
    // Start is called before the first frame update
    void Start()
    {

        //StartCoroutine(Clockwise());
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);
    }

    IEnumerator Clockwise()
    {
        while(degrees < 1200)
        {

            transform.Rotate(0, 1, 0);
            yield return new WaitForSeconds(.1f);
        }
    }
}
