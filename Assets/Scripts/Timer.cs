using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
  public TMP_Text m_TimerDisplay;

  public float m_HourDuration;
  public int m_Hour;
  private float m_t;

    // Start is called before the first frame update
    void Start()
    {
      m_Hour = 10;
      UpdateHourDisplay();
    }

    // Update is called once per frame
    void Update()
    {
      m_t += 1 * Time.deltaTime;
      if(m_t >= m_HourDuration && GameManager.m_instance.m_DisableInput == false)
      {
        m_Hour++;
        UpdateHourDisplay();
        m_t = 0;
        if(m_Hour >= 16)
        {
            UniversalManager um = UniversalManager.m_instance;
            float a = um.m_FriendAActivity + um.m_FriendBActivity + um.m_FriendCActivity + um.m_BossAggression + um.m_VirusAggression;

            if(a >= 5 && um.m_FanOn && um.m_MusicOn)
            {
                GameManager.m_instance.m_EndMan.CreateEnding(11);
            }
            else
            {
                GameManager.m_instance.m_EndMan.CreateEnding(0);
            }
        }
      }
    }

    void UpdateHourDisplay()
    {
      m_TimerDisplay.text = m_Hour.ToString() + ":00";
    }
}
