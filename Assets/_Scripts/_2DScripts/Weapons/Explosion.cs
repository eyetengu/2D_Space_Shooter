using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Animation _explosionAnimation;
    private AudioSource _explosionAudio;

    void Start()
    {

    }

    public void ExplosiveAction()
    {
            
        Destroy(this.gameObject, .1f);
    }

}