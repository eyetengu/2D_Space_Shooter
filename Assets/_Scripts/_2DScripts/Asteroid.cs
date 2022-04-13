using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private SoundManager _soundManager;
    private SpawnManager _spawnManager;
    
    [SerializeField]
    private float _rotateSpeed = 3.0f;
   
    [SerializeField]
    private GameObject _explosionVisual;
    [SerializeField]
    private AudioClip _explosionAudio;
    private AudioSource _audioSource;

    void Start()
    {
        _soundManager = GameObject.Find("Sound_Manager").GetComponent<SoundManager>();
            if(_soundManager == null)
        {
            Debug.LogError("Asteroid.cs- SoundManager Not Found");
        }
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
            if( _spawnManager == null )
        {
            Debug.LogError("Asteroid.cs- SpawnManage is null");
        }

        _audioSource = GetComponent<AudioSource>();
            if(_audioSource == null)
        {
            Debug.LogError("Asteroid.cs- AudioSource is null");
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("If I had a dime for every time somebody did that...");
            Debug.Log("Maybe you should wait until you've had your rest");
            Debug.Log("Time to refill your meds?");
            Debug.Log("In case of intoxication- do NOT play this game");
            Debug.Log("That's ONE way to do it");
            Debug.Log("Stop! That Tickles!");
            Debug.Log("Are You Sure You Know What You're Doing?");
            Debug.Log("Touche");
            Debug.Log("My Impenetrable Skin is Triggerable");
        }
        
        else if(other.tag == "Laser")
        {
            _soundManager.ExplosionSound();
            _spawnManager.StartSpawning();
            Debug.Log("I am ready to explode");
            Instantiate(_explosionVisual,transform.position,Quaternion.identity);
            Destroy(other.gameObject);
            
            Destroy(this.gameObject, .2f);
        }

    }
}
