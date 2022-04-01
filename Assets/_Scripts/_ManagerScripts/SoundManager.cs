using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public AudioClip _explosionAudio;
    public AudioClip _laserAudio;
    public AudioClip _powerupAudio;

    public bool _debugSound;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("P");
            if (_debugSound)
            {
                Debug.Log("Sound");
                ExplosionSound();
            }
        }
    }

    public void ExplosionSound()
    {
        AudioSource.PlayClipAtPoint(_explosionAudio, transform.position, 1f);
    }

    public void LaserSound()
    {
        AudioSource.PlayClipAtPoint(_laserAudio, transform.position, 1f);
    }

    public void PowerUpSound()
    {
        AudioSource.PlayClipAtPoint(_powerupAudio, transform.position, 1f);
    }
}