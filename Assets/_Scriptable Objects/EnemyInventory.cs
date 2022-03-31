using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInventory : MonoBehaviour
{
    public List<EnemySO> enemyDatabase;


    private void Start()
    {
        enemyDatabase.ForEach(i => i.PrintName());   
    }

    
}
