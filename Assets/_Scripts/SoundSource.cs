using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_MyAudioSource;

    bool m_Play;
    bool m_ToggleChange;

    void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
        m_Play = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Play == true && m_ToggleChange == true)
        {
            m_MyAudioSource.Play();
            m_ToggleChange = false;
        }
        if(m_Play == false && m_ToggleChange == true)
        {
            m_MyAudioSource.Stop();
            m_ToggleChange= false;

        }
    }
    void OnGUI()
    {
        m_Play = GUI.Toggle(new Rect(10, 10, 100, 30), m_Play, "PLAY MUSIC");
        if(GUI.changed)
        {
            m_ToggleChange = true;
        }
    }

}
