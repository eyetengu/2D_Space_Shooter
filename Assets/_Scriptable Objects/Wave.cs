using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObject/Wave")]
public class Wave : ScriptableObject
{
    public int[] subWaves;
    public int[] enemyTypes;
    public bool[] readyToSpawn;
    public string[] enemies = { "basic", "smart", "avoider", "firingEnemy", "BOSS", "aggressive", "shields", "newMove"};







    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach ( int subWave in subWaves)
        {
            int waveIndex = 0;
            int[] enemyTypes;
        }
    }
}
