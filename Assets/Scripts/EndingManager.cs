using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingManager : MonoBehaviour
{
    public bool m_EndingCreated;
    public GameObject m_EndingHolder;

    [Header("Elements")]
    public Image m_ShownPicture;
    public TMP_Text m_TitleText;
    public TMP_Text m_DescriptionText;

    [Header("Components")]
    public Sprite[] m_Pictures;
    public string[] m_Titles;
    public string[] m_Descriptions;
    public AudioClip[] m_EndingSongs;
    private float m_Timer = 1f;

    // Start is called before the first frame update
    void Start()
    {
      m_EndingCreated = false;
      m_EndingHolder.SetActive(false);
      GameManager.m_instance.m_EndMan = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_EndingCreated)
        {
            m_Timer -= 1 * Time.deltaTime;
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.m_instance.LoadLevel("Title");
            }
            else if(((Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)) && m_Timer <= 0.0f) || UniversalManager.m_instance.m_SkipEnding == true)
            {
                if(GameManager.m_instance.m_GameLost == true)
                {
                    // Restart Level if lost
                    GameManager.m_instance.LoadLevel("Game");
                }
                else
                {
                    // Go back to title if you won.
                    GameManager.m_instance.LoadLevel("Title");
                }
            }
        }
    }

    public void CreateEnding(int id)
    {
      if(m_EndingCreated == false)
      {
          GameManager.m_instance.m_DisableInput = true;
          GameManager.m_instance.m_FreezeGame = true;

          m_ShownPicture.sprite = m_Pictures[id];
          m_TitleText.text = m_Titles[id];
          m_DescriptionText.text = m_Descriptions[id];

          m_EndingCreated = true;
          m_EndingHolder.SetActive(true);
          GameManager.m_instance.ChangeBGM(m_EndingSongs[id]);
          GameManager.m_instance.MuteAmbience();

          if(id == 0)
          {
              UniversalManager um = UniversalManager.m_instance;

              if(GameManager.m_instance.m_Score > um.m_HighScores[um.m_Day])
              {
                  um.m_HighScores[um.m_Day] = GameManager.m_instance.m_Score;
                  um.SaveScores();
              }
          }
      }
    }
}
