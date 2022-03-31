using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //we are going to use the Spawn Manager to control our enemy waves.
    //we will need an array to house the number of enemies in each wave
    //we will need an int to keep track of the number of enemies spawned in each wave
    //we will need an int to represent the current wave
    
    private UIManager _uiManager;
    private GameManager _gameManager;

    [SerializeField]
    private bool _spawning = true;

    [SerializeField]
    private int[] _waveCounts;          // number Of enemies per Wave;
    private int _currentEnemies;        //number of enemies spawned in current Wave;
    private int _currentWave;           //tells the current wave [index]
    private int _maxWaves;
    private int _currentEnemyMax;

    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject[] _powerups;
    [SerializeField]
    private GameObject[] _enemies;    

    private float _rarePUTimer;

    void Start()
    {
        _spawning = true;

        _enemyContainer = GameObject.Find("EnemyContainer");

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uiManager == null)
        {
            Debug.LogError("UIManager Not Located(SpawnManager)");
        }

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if(_gameManager == null)
        {
            Debug.LogError("SpawnManager.cs- Unable to Locate Game Manager");

        }

        Debug.Log("SpawnManager Ready");
        _maxWaves = _waveCounts.Length;
        _currentEnemyMax = _waveCounts[_currentWave];
    }

    public void StartSpawning()
    {
        if (_spawning == true)      
        {            
            StartCoroutine(WaveStart());

            //StartCoroutine(SpawnPowerUpRoutine());
            //StartCoroutine(SpawnRarePowerUpRoutine());
        }
    }

    public void PlayerDeath()
    {   
        _spawning = true;
        //_uiManager.GameOverMessage();
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (_spawning == false)
        {
            Debug.Log("Spawning Active");
            while (_currentWave < _maxWaves && _currentEnemies < _waveCounts[_currentWave])
            {
                Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 6, 0);
                int randomEnemy = Random.Range(0, 3);
                Instantiate(_enemies[randomEnemy], posToSpawn, Quaternion.identity);
                _currentEnemies++;                          
                
                yield return new WaitForSeconds(1f);
            }

            if(_enemyContainer.transform.childCount < 1)
            {
                _currentWave ++;
                Debug.Log("All Enemies Destroyed");
                _spawning = true;
                _uiManager.UpdateEnemyInfo();
            }

            yield return new WaitForSeconds(.1f);

        }
                StartSpawning();
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while(_spawning == false)
        {
            yield return new WaitForSeconds(Random.Range(5, 8f));

            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 6, 0);

            int randomPowerUp = Random.Range(0,5);
            Instantiate(_powerups[randomPowerUp], posToSpawn, Quaternion.identity);            
            
        }
    }

    IEnumerator SpawnRarePowerUpRoutine()
    {
        while (_spawning == true)
        {

            Debug.Log("SpawnRarePowerUpRoutine");
            _rarePUTimer = Random.Range(20f, 30f);
            yield return new WaitForSeconds(_rarePUTimer);

            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 6, 0);

            Instantiate(_powerups[5], posToSpawn, Quaternion.identity);
            Start();
        }
    }

    IEnumerator WaveStart()
    {        
        _maxWaves = _waveCounts.Length;
        Debug.Log("Current Wave: " + (_currentWave + 1) + " Out of " + _maxWaves);

        if(_currentWave < _waveCounts.Length)
        {            
            _uiManager.UpdateWaveDisplay(_currentWave + 1);         
            _currentEnemies = 0;
            _spawning = false;                       
            yield return new WaitForSeconds(3.0f);
            Debug.Log("Entering SpawnEnemyRoutine");
            
            StartCoroutine(SpawnEnemyRoutine());            
        }
        else
        {
            Debug.Log("end the game");
            _gameManager.GameOver();
            _uiManager.GameOverSequence(2);
        }
        _currentEnemyMax = _waveCounts[_currentWave];
        Debug.Log("Current Max Enemies: " + _currentEnemyMax);
    }
}
