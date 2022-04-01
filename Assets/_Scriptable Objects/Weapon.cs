using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Armory/Weapon")]
[System.Serializable]
public class Weapon : ScriptableObject
{
    public string name;
    public int status;
    public float damageDealt;
    public string damageType;
    public string variant;
    

    //public Weapon(int name, int status, float damageDealt, string damageType, string variant)
    //{
        //this.name = name;
        //this.status = status;
        //this.damageDealt = damageDealt;
        //this.damageType = damageType;
        //this.variant = variant;

    //}





}
