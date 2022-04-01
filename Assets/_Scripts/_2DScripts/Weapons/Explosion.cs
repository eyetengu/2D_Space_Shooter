using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private SoundManager _soundManager;
    private Animation _explosionAnimation;
    //private AudioSource _explosionAudio;

    private GameObject _explosionPrefab;

    void Start()
    {
        _soundManager = GameObject.Find("Sound_Manager").GetComponent<SoundManager>();
        _soundManager.ExplosionSound();
        StartCoroutine(TimeDelay());
    }

    public void ExplosiveAction()
    {
            
        Destroy(this.gameObject, .1f);
    }

    IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);

    }

}
