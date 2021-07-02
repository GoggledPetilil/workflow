using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Virus : MonoBehaviour
{
    [Header("Virus Stats")]
    public GameObject m_VirusObject;
    [Range(0.0f, 1f)]
    public float m_Aggression;
    public float m_InactiveTime;
    private int m_MinWakeTime = 12; // If m_Inactive exceeds m_WakeTime, Virus will spawn.

    [Header("Virus Mechanics")]
    public Slider m_InstallSlider;
    public float m_InstallProgress;
    public bool m_IsInstalling;
    private bool m_Success;
    private int m_Score = 20;

    [Header("Sounds")]
    public AudioSource m_AudioSource;
    public AudioClip m_Notif;

    [Header("Crash Components")]
    public GameObject m_BlueScreen;

    // Start is called before the first frame update
    void Start()
    {
      m_Aggression = UniversalManager.m_instance.m_VirusAggression;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.m_instance.m_FreezeGame == false && m_Aggression > 0.0f)
        {
            if(m_IsInstalling)
            {
                m_InstallProgress += 0.1f * Time.deltaTime;
                m_InstallSlider.value = m_InstallProgress;
                if(m_InstallProgress >= 1f)
                {
                    BlueScreenComputer();
                }
            }
            else
            {
                m_InactiveTime += 1 * Time.deltaTime;
                if(m_InactiveTime >= (m_MinWakeTime / m_Aggression))
                {
                    SpawnVirus();
                }
            }
        }
    }

    void SpawnVirus()
    {
        PlayAudio(m_Notif);

        m_InstallProgress = 0f;
        m_IsInstalling = true;
        m_VirusObject.SetActive(true);

        if(UniversalManager.m_instance.m_DangerIndication == true)
        {
            GameManager.m_instance.m_VirusDanger.SetActive(true);
        }
    }

    public void DisableVirus()
    {
        m_IsInstalling = false;
        m_InactiveTime = 0.0f;
        MenuAnimations ani = m_VirusObject.GetComponent<MenuAnimations>();
        ani.DisableThis();

        float multi = (1f + m_Aggression - m_InstallProgress) / 2;
        GameManager.m_instance.AdjustScore(m_Score * multi);

        if(UniversalManager.m_instance.m_DangerIndication == true)
        {
            GameManager.m_instance.m_VirusDanger.SetActive(false);
        }
    }

    void BlueScreenComputer()
    {
        DisableVirus();
        m_BlueScreen.SetActive(true);
        GameManager.m_instance.m_ComputerBroke = true;
        m_Aggression = 0.0f;
    }

    void PlayAudio(AudioClip clip)
    {
      m_AudioSource.clip = clip;
      m_AudioSource.Play();
    }
}
