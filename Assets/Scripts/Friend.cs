using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Friend : MonoBehaviour
{
    [Header("UI")]
    public string m_Username;
    public Sprite m_Icon;
    public int m_ID;
    public Image m_Fill;
    [Range(0.0f, 1f)]
    public float m_Health; // How much time you have left
    public int m_WordCount;
    public string m_InputText; // To remember what the player "wrote" to them.

    [Header("Mechanics")]
    [Range(0.0f, 1f)]
    public float m_Active; // Effects just how difficult they are to manage.
    [Range(0.0f, 1f)]
    public float m_Impatience; // Multiplier how fast you need to respond
    [Range(0.0f, 1f)]
    public float m_Engagement; // Multiplier how fast they reply
    private int m_ReplyTime = 18; // Minimum time you have to reply in seconds
    private int m_RespondTime = 1; // Minimum time they take to respond in seconds
    public bool m_AreTyping;
    private float m_Timer;
    private int m_Score = 35;

    // Start is called before the first frame update
    void Start()
    {
      m_Health = 1f;
      m_AreTyping = false;
      m_Timer = 0f;

      if(m_Active == 0)
      {
        this.gameObject.SetActive(false);
      }
      else
      {
          GameManager.m_instance.m_Social.CreateMessage(m_Icon, m_ID);
      }
    }

    // Update is called once per frame
    void Update()
    {
      if(m_Active > 0.0f && GameManager.m_instance.m_FreezeGame == false)
      {
        if(m_AreTyping == false && m_Health > 0.0f)
        {
          float step = 1f / m_ReplyTime;
          m_Health -= (((step * m_Impatience) * m_Active) * Time.deltaTime);
          UpdateFill();
        }
        else if(m_AreTyping == true)
        {
          m_Timer += 1f * Time.deltaTime;
          if(m_Timer >= (m_RespondTime / m_Engagement) / m_Active)
          {
            m_AreTyping = false;
            m_Timer = 0f;
            GameManager.m_instance.m_Social.NotifSound();
            GameManager.m_instance.m_Social.CreateMessage(m_Icon, m_ID);
          }
        }
        else if(m_Health <= 0.0f && GameManager.m_instance.m_GameLost == false)
        {
          GameManager.m_instance.GameOver();

          Player.View v = GameManager.m_instance.m_Player.m_Looking;
          switch(v)
          {
              case Player.View.Ceiling:
                GameManager.m_instance.m_EndMan.CreateEnding(1);
                break;
              case Player.View.Computer:
                GameManager.m_instance.m_EndMan.CreateEnding(2);
                break;
              case Player.View.Boss:
                GameManager.m_instance.m_EndMan.CreateEnding(3);
                break;
              case Player.View.Phone:
                GameManager.m_instance.m_EndMan.CreateEnding(4);
                break;
          }
        }
      }
    }

    void UpdateFill()
    {
      m_Fill.fillAmount = m_Health;
      if(m_AreTyping)
      {
        m_Fill.color = new Color(0f, 1f, 1f, 1f);
      }
      else if(m_Health <= 0.25f)
      {
        m_Fill.color = new Color(1f, 0f, 0f, 1f);
      }
      else if(m_Health <= 0.5f)
      {
        m_Fill.color = new Color(1f, 1f, 0f, 1f);
      }
      else
      {
        m_Fill.color = new Color(0f, 1f, 0f, 1f);
      }
    }

    public void ReceiveReply()
    {
      float multi = (m_Health + m_Active) / 2;
      GameManager.m_instance.AdjustScore(m_Score * multi);

      m_Health = 1f;
      m_AreTyping = true;
      UpdateFill();
      m_WordCount = 0;
      m_InputText = "";
    }

    void OnMouseOver()
    {
      if(Input.GetMouseButtonUp(0)){
        GameManager.m_instance.m_Social.SwitchDMS(m_ID);
      }
    }
}
