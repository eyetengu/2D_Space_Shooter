using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipCalculator : MonoBehaviour
{
    //a variable tip amount that is public
    //amount of the total bill
    public float billTotal;
    public float tipRate;
    private float tipDecimal;
    public float tipAmount;
    private float totalDue;
    // Start is called before the first frame update
    void Start()
    {
        tipDecimal = tipRate/100;

        tipAmount = billTotal * tipDecimal;
        totalDue = billTotal + tipAmount;
        Debug.Log("Your Bill Total is: $" + billTotal);
        Debug.Log("A " + tipRate + "% tip would be $" + tipAmount);
        Debug.Log("Total Due: " + totalDue);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
