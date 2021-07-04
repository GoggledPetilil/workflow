using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Mechanics")]
    [Range(0.0f, 1f)]
    public float m_Aggression; // Effects just how difficult they are to manage.
    [Range(0, 4)]
    public int m_Phase; // How close they are to you.
    [Range(0.0f, 1f)]
    public float m_Anger; // In a way, the player's "health." If it's full, they lose.
    private float m_MinPhaseTime = 5f; // Minimum time Boss spends each phase.
    private float m_LookingTime = 3f; // Time Boss looks at you (Not effected by Aggression).
    private float m_Timer;
    private float m_TimeBonus; // Randomized bonus added to the phase time.
    private int m_Score = 25;

    [Header("Graphics")]
    public SpriteRenderer m_Sprite;
    public Sprite[] m_PhaseSprite;

    [Header("Audio")]
    public AudioSource m_Audio;
    public AudioClip m_Breathing;

    void Awake()
    {
      m_Audio = GetComponent<AudioSource>();
      m_Audio.clip = m_Breathing;
    }

    // Start is called before the first frame update
    void Start()
    {
      m_Audio.volume = 0f;
      m_Phase = 0;
      UpdatePhase();

      m_Aggression = UniversalManager.m_instance.m_BossAggression;
    }

    // Update is called once per frame
    void Update()
    {
      if(GameManager.m_instance.m_FreezeGame == false && m_Aggression > 0.0f)
      {
        // Boss is moving.
        m_Timer += 1f * Time.deltaTime;
        if(m_Timer >= ((m_MinPhaseTime / m_Aggression) + m_TimeBonus) && m_Phase < 4)
        {
            // Boss progresses through his phases,
            // but hasn't reached the Player yet.
            m_Phase++;
            UpdatePhase();
        }
        else if(m_Phase >= 4)
        {
            // Boss is currently looking at the Player.
            // If they are not looking at the Computer, his anger increases.
            if(GameManager.m_instance.m_Player.m_Looking != Player.View.Computer)
            {
                m_Anger += 1f * Time.deltaTime;
            }


            if(m_Timer >= (m_LookingTime + m_TimeBonus))
            {
              // Look Timer has passed,
              // Boss goes away again.
              float multi = (1f + m_Aggression - m_Anger) / 2;
              GameManager.m_instance.AdjustScore(m_Score * multi);

              GameManager.m_instance.m_BossDanger.SetActive(false);

              m_Phase = 0;
              m_Anger = 0.0f;
              UpdatePhase();
            }
            else if((m_Anger >= 1f || GameManager.m_instance.m_ComputerBroke) && GameManager.m_instance.m_GameLost == false)
            {
                // Player is dead as hell
                KillsPlayer();
            }
        }
      }
    }

    void UpdatePhase()
    {
        m_Timer = 0f;
        m_Sprite.sprite = m_PhaseSprite[m_Phase];
        if(m_Phase == 3)
        {
          m_Audio.volume = 0.1f;
        }
        else if(m_Phase == 4)
        {
          m_Audio.volume = 1f;
          if(UniversalManager.m_instance.m_DangerIndication == true)
          {
              GameManager.m_instance.m_BossDanger.SetActive(true);
          }
        }
        else
        {
          m_Audio.volume = 0.0f;
        }
        m_Audio.Play();

        m_TimeBonus = Random.Range(-1f, 3f);
    }

    void KillsPlayer()
    {
      // Boss saw Player slacking. Game Over.
      GameManager.m_instance.GameOver();

      m_Audio.Stop();

      if(GameManager.m_instance.m_ComputerBroke)
      {
          GameManager.m_instance.m_EndMan.CreateEnding(8);
      }
      else
      {
          Player.View v = GameManager.m_instance.m_Player.m_Looking;
          switch(v)
          {
              case Player.View.Ceiling:
                GameManager.m_instance.m_EndMan.CreateEnding(5);
                break;
              case Player.View.Boss:
                GameManager.m_instance.m_EndMan.CreateEnding(6);
                break;
              case Player.View.Phone:
                GameManager.m_instance.m_EndMan.CreateEnding(7);
                break;
          }
      }
    }
}
