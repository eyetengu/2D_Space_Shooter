using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Explosive.asset", menuName = "Armory/Explosive")]
[System.Serializable]
public class Explosive : Weapon
{
//Timer
    public bool hasTimer;

//Detonatable    
    public bool hasDetonator;
    public bool detonatorTriggersTimer;

//On Impact
    public bool onImpact;
    public bool impactTriggersTimer;

//Tripwire or altitude triggered
    public bool onTrigger;

//Area of effect
    public float blastRadius;


    
}
