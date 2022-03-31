using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    
    public CanvasBehavior _uiManager;           //added to facilitate transfer of UIMessages 

    [SerializeField]
    private List<Wave> _waves = new List<Wave>();   //List is the type of data container. 
                                                    //<Wave> refers to the Wave(ScriptableObject)
                                                    //_waves is the nickname for the List data container
                                                    //this line creates a new list for us to use

    private int _currentWave;                   //int value to store the current wave number

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<CanvasBehavior>();  //make component accessible through its nickname
        _uiManager.PrintWaveName(_currentWave);         //Send over wave number for display on the UI
        StartCoroutine(StartWaveRoutine());             //Begin the Coroutine to start the wave
    }

    IEnumerator StartWaveRoutine()
    {
        while(true)
        {
            var currentWave = _waves[_currentWave].sequence;    

            var previousWave = new GameObject("PreviousWave");

            foreach(var obj in currentWave)
            {
                Instantiate(obj, previousWave.transform);
                yield return new WaitForSeconds(1.0f); 
            }
            yield return new WaitForSeconds(5.0f);

            Destroy(previousWave);
            
            if(_currentWave == _waves.Count)
            {
                Debug.Log("Finished Waves!");
            }
            _currentWave++;
            _uiManager.PrintWaveName(_currentWave);
        }
        //waveName = "Game Over";
        _uiManager.PrintWaveName(_currentWave);
    }

    // Update is called once per frame
    public void PrintName()
    {    }
}
