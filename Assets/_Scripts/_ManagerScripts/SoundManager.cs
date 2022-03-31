using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public AudioClip _explosionAudio;
    public AudioClip _laserAudio;
    public AudioClip _powerupAudio;

    public void Update()
    {
        
    }

    public void ExplosionSound()
    {
        AudioSource.PlayClipAtPoint(_explosionAudio, transform.position, .1f);
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