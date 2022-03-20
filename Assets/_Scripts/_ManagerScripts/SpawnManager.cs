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
    private bool _stopSpawning = false;

    private UIManager _uiManager;


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
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 6, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            
            yield return new WaitForSeconds(5.0f);
        }
    }
    IEnumerator SpawnPowerUpRoutine()
    {
        while(_stopSpawning == false)
        {

            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 6, 0);

            int randomPowerUp = Random.Range(0,3);
            Instantiate(_powerups[randomPowerUp], posToSpawn, Quaternion.identity);            
            
            yield return new WaitForSeconds(Random.Range(5, 8f));
        }
    }


}
