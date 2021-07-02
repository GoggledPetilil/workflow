using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour
{
    public AudioSource m_Audio;
    public AudioClip m_Clip;

    // Start is called before the first frame update
    void Start()
    {
        if(UniversalManager.m_instance.m_MusicOn)
        {
            m_Audio.clip = m_Clip;
            m_Audio.Play();
            Destroy(this);
        }
    }
}
