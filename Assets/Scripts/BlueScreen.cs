using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlueScreen : MonoBehaviour
{
    public TMP_Text m_Progress;
    private float m_ProgressTimer;
    private float m_Timer;
    private bool stop;

    public AudioSource m_Audio;
    public AudioClip m_Explosion;

    void Update()
    {
        if(!stop)
        {
            if(m_Timer <= 0.0)
            {
                int i = Random.Range(0, 2);
                if(i == 0)
                {
                    m_ProgressTimer += 5;
                }
                else
                {
                    m_ProgressTimer += 10;
                }

                if(m_ProgressTimer < 95)
                {
                    m_Progress.text = m_ProgressTimer.ToString("F0") + "% complete";
                }
                else
                {
                    m_Audio.clip = m_Explosion;
                    m_Audio.Play();
                    GameManager.m_instance.m_EndMan.CreateEnding(9);
                    stop = true;
                }

                m_Timer = 2.5f;
            }
            else if(!GameManager.m_instance.m_FreezeGame || !GameManager.m_instance.m_GameLost)
            {
                m_Timer -= 1 * Time.deltaTime;
            }
        }
    }
}
