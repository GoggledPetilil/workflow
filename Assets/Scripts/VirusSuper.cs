using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSuper : MonoBehaviour
{
    [Header("Virus Components")]
    public SpriteRenderer m_ComputerBG;
    public GameObject m_VirusGraphic;
    public GameObject m_ScreenCover;

    [Header("Virus Mechanics")]
    private float m_Timer;
    private float m_WaitTime = 12;
    private bool m_HasSpawned;
    private bool m_Staaaare;
    private int m_ScreenFlashes = 6;
    private float m_TransTime = 0.5f;

    [Header("Sounds")]
    public AudioSource m_AudioSource;
    public AudioClip m_Notif;

    // Start is called before the first frame update
    void Start()
    {
        UniversalManager um = UniversalManager.m_instance;
        float d = um.m_FriendAActivity + um.m_FriendBActivity + um.m_FriendCActivity + um.m_BossAggression + um.m_VirusAggression;

        int r = Random.Range(0, 1000);

        if((um.m_MusicOn == false && d <= 0.0) || r == 0)
        {
            // Bad Stuff
            GameManager.m_instance.m_Social.LockScreen();
            GameManager.m_instance.m_FreezeGame = true;
            GameManager.m_instance.m_UhOh = true;
            GameManager.m_instance.MuteAmbience();
            m_ComputerBG.color = new Color(0f, 0f, 0f, 1f);
        }
        else
        {
            Destroy(m_VirusGraphic);
            Destroy(m_ScreenCover);
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(m_HasSpawned && GameManager.m_instance.m_Player.m_Looking == Player.View.Computer && !m_Staaaare)
        {
            GameManager.m_instance.m_DisableInput = true;
            m_Staaaare = true;
        }
        else
        {
            m_Timer += 1f * Time.deltaTime;
            if(m_Timer >= m_WaitTime)
            {
                if(!m_HasSpawned)
                {
                    SpawnVirus();
                }
                else if(m_Staaaare)
                {
                    AudioListener.volume = 0.0f;
                    m_ScreenCover.SetActive(true);
                    Application.Quit();
                    Debug.Log("Game has quit.");
                    Destroy(this);
                }
            }
        }
    }

    void SpawnVirus()
    {
        m_AudioSource.clip = m_Notif;
        m_AudioSource.Play();

        m_HasSpawned = true;
        m_VirusGraphic.SetActive(true);
        m_Timer = 0.0f;

        if(UniversalManager.m_instance.m_DangerIndication == true)
        {
            GameManager.m_instance.m_VirusDanger.SetActive(true);
        }
    }
}
