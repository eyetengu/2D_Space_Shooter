using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 3.0f;

    [SerializeField]
    private GameObject _explosionVisual;

    [SerializeField]
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _explosionAudio;
    private SoundManager _soundManager;

    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
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
        //rotate object on the z axis
        //use a speed of 3
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
            Debug.Log("Asteroid- collision Laser");
            _soundManager.ExplosionSound();
            //""""OnExplosion""""
            Instantiate(_explosionVisual,transform.position,Quaternion.identity);

            //_audioSource.PlayOneShot(_explosionAudio, 4f);
            Destroy(other.gameObject);
            
            _spawnManager.StartSpawning();

            Destroy(this.gameObject, .2f);
        }
    }
}
