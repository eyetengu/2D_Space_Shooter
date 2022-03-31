using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObject/Wave")]
public class Wave : ScriptableObject
{
    public string waveName = "wave.cs";
    public List<GameObject> sequence;




    //enemy types- public string[] enemies = { "basic", "smart", "avoider", "firingEnemy", "BOSS", "aggressive", "shields", "newMove"};
}

