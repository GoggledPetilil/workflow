using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalManager : MonoBehaviour
{
    [Header("Main Elements")]
    public static UniversalManager m_instance;

    [Header("Settings")]
    public float m_UnimVolume;
    public bool m_HoldType; // If True, Player only needs to hold in order to type.
    public bool m_DangerIndication; // If true, graphic will show up when in danger.
    public bool m_SkipEnding; // Skips any ending, go straight to gameplay.

    [Header("Game Stats")]
    public int m_Day;
    public int[] m_HighScores; // 0 is for the custom day.
    public float m_FriendAActivity;
    public float m_FriendBActivity;
    public float m_FriendCActivity;
    public float m_BossAggression;
    public float m_VirusAggression;
    public bool m_FanOn;
    public bool m_MusicOn; // Has been changed to a crowd instead

    [Header("Sound Elements")]
    public AudioSource m_AudioPlayer;
    public AudioClip m_MenuPopUp;
    public AudioClip m_MenuPopDown;
    public AudioClip m_Confirm;
    public AudioClip m_Start;

    void Awake()
    {
      if(m_instance == null)
      {
        m_instance = this;
        LoadPlayerPrefs();
        DontDestroyOnLoad(this.gameObject);
      }
      else
      {
          Destroy(this.gameObject);
        }
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("volume", m_UnimVolume);
        PlayerPrefs.SetInt("canHoldType",(m_HoldType ? 1 : 0));
        PlayerPrefs.SetInt("showDanger", (m_DangerIndication ? 1 : 0));
        PlayerPrefs.SetInt("skipEndings", (m_SkipEnding ? 1 : 0));
    }

    public void SaveScores()
    {
        PlayerPrefs.SetInt("highScoreCustom", m_HighScores[0]);
        PlayerPrefs.SetInt("highScoreOne", m_HighScores[1]);
        PlayerPrefs.SetInt("highScoreTwo", m_HighScores[2]);
        PlayerPrefs.SetInt("highScoreThree", m_HighScores[3]);
        PlayerPrefs.SetInt("highScoreFour", m_HighScores[4]);
        PlayerPrefs.SetInt("highScoreFive", m_HighScores[5]);
    }

    public void LoadPlayerPrefs()
    {
        m_UnimVolume = PlayerPrefs.GetFloat("volume", 1f);
        m_HoldType = (PlayerPrefs.GetInt("canHoldType") != 0);
        m_DangerIndication = (PlayerPrefs.GetInt("showDanger") != 0);
        m_SkipEnding = (PlayerPrefs.GetInt("skipEndings") != 0);

        m_HighScores[0] = PlayerPrefs.GetInt("highScoreCustom");
        m_HighScores[1] = PlayerPrefs.GetInt("highScoreOne");
        m_HighScores[2] = PlayerPrefs.GetInt("highScoreTwo");
        m_HighScores[3] = PlayerPrefs.GetInt("highScoreThree");
        m_HighScores[4] = PlayerPrefs.GetInt("highScoreFour");
        m_HighScores[5] = PlayerPrefs.GetInt("highScoreFive");
    }

    public void PlaySound(AudioClip clip)
    {
        m_AudioPlayer.clip = clip;
        m_AudioPlayer.Play();
    }

    public void LoadLevel(string sceneName)
    {
        LevelLoader g = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        LoadLevel(sceneName);
    }
}
