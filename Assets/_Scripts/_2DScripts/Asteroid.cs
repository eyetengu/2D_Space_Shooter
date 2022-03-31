using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private SpawnManager _spawnManager;
    private SoundManager _soundManager;
   
    [SerializeField]
    private float _rotateSpeed = 3.0f;

    [SerializeField]
    private GameObject _explosionVisual;

    [SerializeField]
    private AudioClip _explosionAudio;
    private AudioSource _audioSource;

    void Start()
    {
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

        _soundManager = GameObject.Find("Sound_Manager").GetComponent<SoundManager>();
        if(_soundManager == null)
        {
            Debug.LogError("Asteroid.cs- SoundManager Not Found");
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
            _spawnManager.StartSpawning();
            //Debug.Log("Asteroid- collision Laser");
            _soundManager.ExplosionSound();

            Instantiate(_explosionVisual,transform.position,Quaternion.identity);

            Destroy(other.gameObject);
            
            //Debug.Log("Hit by laser");
            Destroy(this.gameObject, .2f);
        }
        else if(other.transform.tag == "Bomb")
        {
            //Debug.Log("Presence sensed at asteroid");
        }
    }
}
