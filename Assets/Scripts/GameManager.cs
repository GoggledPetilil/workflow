using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Main Elements")]
    public static GameManager m_instance;
    public LevelLoader m_LevelLoader;
    public EndingManager m_EndMan;
    public AudioSource m_BGM;
    public Player m_Player;
    public bool m_DisableInput; // Disables any Player Input
    public bool m_FreezeGame; // Prevents any NPC movement

    [Header("UI Elements")]
    public GameObject m_TimeUI;
    public Canvas m_FriendUI;
    public Canvas m_SocialUI;
    public GameObject m_PauseMenu;
    public TMP_Text m_ScoreText;
    public GameObject m_DangerCanvas;
    public GameObject m_BossDanger;
    public GameObject m_VirusDanger;
    public GameObject m_CrashCanvas;
    public GameObject m_LockScreen;

    [Header("Game Elements")]
    public bool m_GameLost;
    public bool m_ComputerBroke;
    public bool m_UhOh;
    public SocialMedia m_Social;
    public int m_WordReq;
    public int m_Score;
    public AudioSource m_Ambience;
    public AudioSource m_Fan;
    public AudioSource m_Crowd;

    void Awake()
    {
      if(m_instance == null)
      {
        m_instance = this;
        m_GameLost = false;
      }
    }

    public void GameOver()
    {
      if(m_GameLost == false)
      {
        m_GameLost = true;
        m_DisableInput = true;
        m_FreezeGame = true;
      }
    }

    public void PauseGame(bool state)
    {
        if(!m_UhOh)
        {
            m_DisableInput = state;
            m_FreezeGame = state;

            m_PauseMenu.SetActive(state);

            AudioListener.pause = state;
        }
    }

    public void ToggleTimeDisplay(bool state)
    {
        m_TimeUI.SetActive(state);
    }

    public void ToggleFriendDisplay(bool state)
    {
        m_FriendUI.enabled = state;
    }

    public void ToggleSocialDisplay(bool state)
    {
        m_SocialUI.enabled = state;
    }

    public void ToggleDangerCanvas(bool state)
    {
        m_DangerCanvas.SetActive(state);
    }

    public void ToggleCrashCanvas(bool state)
    {
        m_CrashCanvas.SetActive(state);
    }

    public void ToggleLockScreen(bool state)
    {
        m_LockScreen.SetActive(state);
    }

    public void SwitchPlayerView()
    {
        Player.View v = m_Player.m_Looking;
        switch (v)
        {
            case Player.View.Phone:
              ToggleFriendDisplay(true);
              ToggleSocialDisplay(true);
              ToggleTimeDisplay(true);
              ToggleLockScreen(true);

              ToggleCrashCanvas(false);

              ToggleDangerCanvas(true);
              break;
            case Player.View.Computer:
              ToggleFriendDisplay(false);
              ToggleSocialDisplay(false);
              ToggleTimeDisplay(true);
              ToggleLockScreen(false);

              ToggleCrashCanvas(true);

              ToggleDangerCanvas(true);
              break;
            case Player.View.Boss:
              ToggleFriendDisplay(false);
              ToggleSocialDisplay(false);
              ToggleTimeDisplay(false);
              ToggleLockScreen(false);

              ToggleCrashCanvas(false);

              ToggleDangerCanvas(true);
              break;
            case Player.View.Ceiling:
              ToggleFriendDisplay(false);
              ToggleSocialDisplay(false);
              ToggleTimeDisplay(false);
              ToggleLockScreen(false);

              ToggleCrashCanvas(false);

              ToggleDangerCanvas(false);
              break;
        }
    }

    public void ChangeBGM(AudioClip audio)
    {
        m_BGM.clip = audio;
        m_BGM.Play();
    }

    public void AdjustScore(float addition)
    {
        m_Score += Mathf.RoundToInt(addition) * 10;
        m_ScoreText.text = "Score: " + m_Score.ToString();
    }

    public void MuteAmbience()
    {
        m_Ambience.volume = 0.0f;
        m_Fan.volume = 0.0f;
        m_Crowd.volume = 0.0f;
    }

    public void LoadLevel(string sceneName)
    {
        m_LevelLoader.LoadLevel(sceneName);
    }
}
