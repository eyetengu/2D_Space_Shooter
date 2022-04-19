using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObjectSY", menuName = "ScriptableObjects/EnemySY")]
public class EnemyScriptableObjectSAMYAM : ScriptableObject 
{
    public int health = 100;
    public float speed = 3f;
    public EnemyAttackScriptableObjectSAMYAM enemyAttackType;

    private void Awake()
    {
        
        Debug.Log("Awake- Enemy");
    }
    private void OnEnable()
    {
        Debug.Log("OnEnable- Enemy");
    }
    private void OnDisable()
    {
        Debug.Log("OnDisable- Enemy");
    }
    private void OnDestroy()
    {
        Debug.Log("OnDestroy- Enemy");
    }
    private void OnValidate()
    {
        Debug.Log("OnValidate- Enemy");
    }

}
