using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasBehavior : MonoBehaviour
{
    private Text _messageText;

    private string _waveName = "Having Problems?";


    void Start()
    {
        _messageText = GameObject.Find("Message_text").GetComponent<Text>();
        if(_messageText == null)
        {
            Debug.LogError("No Component Found");
        }
        _messageText.text = "Goodbye Cruel World" + " " + _waveName;
        
        }

    // Update is called once per frame
    public void PrintWaveName(int _currentWave)
    {
        int current = _currentWave++;
        Debug.Log("Current Wave: " + current++);
        _messageText.text = "Wave: " + current++;
        StartCoroutine(ClearMessage());
    }

    IEnumerator ClearMessage()
    {
        yield return new WaitForSeconds(2);
        //_messageText.text = "";

        yield return new WaitForSeconds(2);
        _messageText.text = " ";
    }
}
