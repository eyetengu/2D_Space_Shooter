using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject[] _powerups;
    [SerializeField]
    private GameObject[] _enemies;
    
    [SerializeField]
    private bool _stopSpawning = false;

    private UIManager _uiManager;

    private float _rarePUTimer;

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uiManager == null)
        {
            Debug.Log("UIManager Not Located(SpawnManager)");
        }
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
        StartCoroutine(SpawnRarePowerUpRoutine());
    }

    public void PlayerDeath()
    {   
        _stopSpawning = true;
        //_uiManager.GameOverMessage();
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while(_stopSpawning == false)
        {
            yield return new WaitForSeconds(.5f);

            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 6, 0);

            int randomEnemy = Random.Range(0, 3);
            Instantiate(_enemies[randomEnemy], posToSpawn, Quaternion.identity);
            foreach(GameObject enemy in _enemies)
            {
                transform.parent = _enemyContainer.transform;
            }
            
            yield return new WaitForSeconds(2.5f);
        }
    }
    IEnumerator SpawnPowerUpRoutine()
    {
        while(_stopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(5, 8f));

            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 6, 0);

            int randomPowerUp = Random.Range(0,5);
            Instantiate(_powerups[randomPowerUp], posToSpawn, Quaternion.identity);            
            
        }
    }

    IEnumerator SpawnRarePowerUpRoutine()
    {
        while (_stopSpawning == false)
        {

            Debug.Log("SpawnRarePowerUpRoutine");
            _rarePUTimer = Random.Range(20f, 30f);
            yield return new WaitForSeconds(_rarePUTimer);

            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 6, 0);

            Instantiate(_powerups[5], posToSpawn, Quaternion.identity);
            Start();
        }
    }

}
