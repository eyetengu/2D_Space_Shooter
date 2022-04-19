using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttackScriptableObject", menuName = "ScriptableObjects/EnemyAttackSY")]
public class EnemyAttackScriptableObjectSAMYAM : ScriptableObject
{
    public float attackDamage = 20f;
    public float attackRange = 10f;
    public AudioClip attackSound;    
}
