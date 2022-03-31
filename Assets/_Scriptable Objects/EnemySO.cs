using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy.asset", menuName= "EnemyInventory/Enemy")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public int itemID;
    public Sprite _icon;
    public int gold;

    public void PrintName()
    {
        Debug.Log("Enemy: " + enemyName);
    }
    
}
