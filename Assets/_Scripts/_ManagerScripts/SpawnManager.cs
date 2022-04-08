using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{   
    private UIManager _uiManager;
    private GameManager _gameManager;

    [SerializeField]
    private bool _spawning = true;
    private bool _okToSpawnPowerups = true;

    [SerializeField]
    private bool _spawnPowerUp = true;
    //[SerializeField]
    //private bool _spawnRarePowerUp = true;
    //[SerializeField]
    //private bool _spawnEpicPowerUp = true;
    [SerializeField]
    private bool _spawnNegativePowerUp = true;

    [SerializeField]
    private float _basicDelay = 5f;
    [SerializeField]
    private float _rareDelay = 10f;
    [SerializeField]
    private float _epicDelay = 15f;
    [SerializeField]
    private float _negativeDelay = 6f;


    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private int[] _waveCounts;          // number Of enemies per Wave;
    private int _currentEnemies;        //number of enemies spawned in current Wave;
    private int _currentWave;           //tells the current wave [index]
    private int _maxWaves;
    private int _currentEnemyMax;

    [SerializeField]
    private GameObject[] _enemies;

    [SerializeField]
    private GameObject[] _basicPowerups;
    [SerializeField]
    private GameObject[] _rarePowerups;
    [SerializeField]
    private GameObject[] _epicPowerups;
    [SerializeField]
    private GameObject[] _negativePowerups;

    void Start()
    {
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

        _maxWaves = _waveCounts.Length;
        _currentEnemyMax = _waveCounts[_currentWave];
    }

    public void Update()
    {
        if (_spawnPowerUp == true && _okToSpawnPowerups == true)
        {
            StartCoroutine(SpawnPowerUpRoutine());
        }
            
        if(_spawnNegativePowerUp == true && _okToSpawnPowerups == true)
        {
            StartCoroutine(SpawnNegativePowerUpRoutine());
        }    
    }

    public void StartSpawning()
    {
        if (_spawning == true)      
        {            
            StartCoroutine(WaveStart());
        }
    }
    public void PlayerDeath()
    {
        _okToSpawnPowerups = false;
        _spawnPowerUp = false;
        _spawnNegativePowerUp = false;
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
            
            StartCoroutine(SpawnEnemyRoutine());            
        }
        else 
        {
            Debug.Log("end the game");
            _gameManager.GameOver();
            _uiManager.GameOverSequence(2);
        }

        _currentEnemyMax = _waveCounts[_currentWave];
        if(_currentEnemyMax < 1)
        { _currentEnemyMax = 0; }

        Debug.Log("Current Max Enemies: " + _currentEnemyMax);
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
        _spawnPowerUp = false;
        int randomSpawn = Random.Range(1, 101);
        Vector3 posToSpawn = new Vector3(Random.Range(-8, 8), 8, 0);

        if(randomSpawn < 50)
        {
            int randomBasic = Random.Range(0, _basicPowerups.Length);
            Instantiate(_basicPowerups[randomBasic], posToSpawn, Quaternion.identity);
        }
        else if(randomSpawn < 90)
        {
            int randomRare = Random.Range(0, _rarePowerups.Length);
            Instantiate(_rarePowerups[randomRare], posToSpawn, Quaternion.identity);
        }
        else if(randomSpawn < 100)
        {
            int randomEpic = Random.Range(0, _epicPowerups.Length);

            Instantiate(_epicPowerups[0], posToSpawn, Quaternion.identity);
        }
        else
        {
            Instantiate(_basicPowerups[2]);
        }

        yield return new WaitForSeconds(5f);
                    
        _spawnPowerUp = true;
    }
   
    IEnumerator SpawnNegativePowerUpRoutine()
    {
        _spawnNegativePowerUp = false;

        Vector3 posToSpawnNegative = new Vector3(Random.Range(-8, 8f), 7, 0);

        yield return new WaitForSeconds(5f);

        Instantiate(_negativePowerups[0], posToSpawnNegative, Quaternion.identity);
        yield return new WaitForSeconds(1f);

        _spawnNegativePowerUp= true;


    }

    public void StopSpawning()
    {
        _spawning = true;
        _spawnPowerUp = false;
        _spawnNegativePowerUp = false;
    }
}
