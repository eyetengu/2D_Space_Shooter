using UnityEngine;

public class InstantiateScriptableObjectSY : MonoBehaviour
{
    EnemyScriptableObjectSAMYAM test;
    
    void Start()
    {
        Debug.Log("EnemySOSamyam");
        test = (EnemyScriptableObjectSAMYAM)ScriptableObject.CreateInstance(typeof(EnemyScriptableObjectSAMYAM));
        //Debug.Log(test.enemyAttackType.attackDamage);
        ScriptableObject.Destroy(test, 4);
    }
}
