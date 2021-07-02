using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SocialMedia : MonoBehaviour
{
    public enum Contacts
    {
      FriendA,
      FriendB,
      FriendC
    }

    [Header("View")]
    public Contacts m_DMing;

    [Header("Contacts")]
    public Friend A;
    public Friend B;
    public Friend C;

    [Header("UI")]
    public Image m_Fill;
    public Image m_SendButton;
    public TMP_Text m_ContactName;
    public GameObject m_LockScreen;

    [Header("DMs")]
    public int m_MaxMessages;
    public GameObject[] m_DMsDisplay;
    public GameObject m_MessagePrefab;
    public Sprite[] m_MessageBackdrops;

    [Header("Sounds")]
    private AudioSource m_TypingSource;
    public AudioSource m_NotifSource;
    public AudioClip m_TypingSFX;
    public AudioClip m_NotificationSFX;
    public AudioClip m_VibrateSFX;

    [Header("Typing")]
    private int m_CharacterLimit = 28;
    public TMP_Text m_TextField;
    public string m_Char; // What will actually be displayed.
    private bool m_canHold;
    private float m_HoldTimer;
    private float m_TimerLimit = 0.085f;

    void Awake()
    {
      m_TypingSource = GetComponent<AudioSource>();

      A.m_Active = UniversalManager.m_instance.m_FriendAActivity;
      B.m_Active = UniversalManager.m_instance.m_FriendBActivity;
      C.m_Active = UniversalManager.m_instance.m_FriendCActivity;

      m_canHold = UniversalManager.m_instance.m_HoldType;
    }

    // Start is called before the first frame update
    void Start()
    {
      m_TextField.text += "";
      GameManager.m_instance.m_Social = this;

      // Switch to first active friend.
      if(A.m_Active > 0.0f)
      {
          SwitchDMS(0);
      }
      else if(B.m_Active > 0.0f)
      {
          SwitchDMS(1);
      }
      else if(C.m_Active > 0.0f)
      {
          SwitchDMS(2);
      }
      else
      {
          m_LockScreen.SetActive(true);
          this.enabled = false;
      }
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.anyKeyDown && !m_canHold && GameManager.m_instance.m_DisableInput == false && !(Input.GetMouseButtonDown(0) ||
      Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
      Input.GetKeyDown(KeyCode.Escape)) && GameManager.m_instance.m_Player.m_Looking == Player.View.Phone)
      {
        switch(m_DMing)
        {
          case Contacts.FriendA:
            TypingToFriend(A);
            break;
          case Contacts.FriendB:
            TypingToFriend(B);
            break;
          case Contacts.FriendC:
            TypingToFriend(C);
            break;
        }
      }
      else if(m_canHold && Input.GetMouseButton(0) && GameManager.m_instance.m_DisableInput == false && GameManager.m_instance.m_Player.m_Looking == Player.View.Phone)
      {
          m_HoldTimer += 1 * Time.deltaTime;
          if(m_HoldTimer >= m_TimerLimit)
          {
              m_HoldTimer = 0.0f;
              switch(m_DMing)
              {
                case Contacts.FriendA:
                  TypingToFriend(A);
                  break;
                case Contacts.FriendB:
                  TypingToFriend(B);
                  break;
                case Contacts.FriendC:
                  TypingToFriend(C);
                  break;
              }
          }
      }
    }

    void TypingToFriend(Friend friend)
    {
      if(friend.m_AreTyping == false)
      {
        friend.m_WordCount++;
        AdjustFillImage(friend.m_WordCount);
        TypeSound();
        AdjustTextField();
        friend.m_InputText = m_TextField.text;
      }
    }

    public void SwitchDMS(int ID)
    {
      switch(ID)
      {
        case(0):
          m_DMing = Contacts.FriendA;
          m_ContactName.text = "@" + A.m_Name;
          m_TextField.text = A.m_InputText;
          AdjustFillImage(A.m_WordCount);
          HideAllDMs();
          break;
        case(1):
          m_DMing = Contacts.FriendB;
          m_ContactName.text = "@" + B.m_Name;
          m_TextField.text = B.m_InputText;
          AdjustFillImage(B.m_WordCount);
          HideAllDMs();
          break;
        case(2):
          m_DMing = Contacts.FriendC;
          m_ContactName.text = "@" + C.m_Name;
          m_TextField.text = C.m_InputText;
          AdjustFillImage(C.m_WordCount);
          HideAllDMs();
          break;
      }
      m_DMsDisplay[ID].SetActive(true);
    }

    public void HideAllDMs()
    {
        m_DMsDisplay[0].SetActive(false);
        m_DMsDisplay[1].SetActive(false);
        m_DMsDisplay[2].SetActive(false);
    }

    public void AdjustFillImage(int words)
    {
      if(words >= GameManager.m_instance.m_WordReq)
      {
        m_SendButton.enabled = true;
      }
      else
      {
        m_SendButton.enabled = false;
      }

      float m_amount = (1f / (float)GameManager.m_instance.m_WordReq);
      m_Fill.fillAmount = m_amount * (float)words;
    }

    public void SendMessage()
    {
      if(m_SendButton.enabled == true)
      {
        int i = -1;
        switch(m_DMing)
        {
          case Contacts.FriendA:
            A.ReceiveReply();
            i = A.m_ID;
            break;
          case Contacts.FriendB:
            B.ReceiveReply();
            i = B.m_ID;
            break;
          case Contacts.FriendC:
            C.ReceiveReply();
            i = C.m_ID;
            break;
        }
        AdjustFillImage(0);
        m_TextField.text = "";
        TypeSound();
        CreateMessage(GameManager.m_instance.m_Player.m_Icon, i);
      }
    }

    public void CreateMessage(Sprite icon, int FriendID)
    {
        int i = Random.Range(0, m_MessageBackdrops.Length);
        GameObject g = Instantiate(m_MessagePrefab) as GameObject;
        Message m = g.GetComponent<Message>();

        g.transform.SetParent(m_DMsDisplay[FriendID].transform, false);
        if(m_DMsDisplay[FriendID].transform.childCount > m_MaxMessages)
        {
            Destroy(m_DMsDisplay[FriendID].transform.GetChild(0).gameObject);
        }

        m.m_Icon = icon;
        m.m_Backdrop = m_MessageBackdrops[i];
        m.UpdateGraphics();
    }

    public void AdjustTextField()
    {
        if(m_TextField.text.Length >= m_CharacterLimit || m_TextField.text.Length == 0)
        {
            m_TextField.text = m_Char;
        }
        else
        {
            string s = m_TextField.text;
            string lastchar = s.Substring(s.Length - 1, 1);
            int r = Random.RandomRange(0, 2);

            if(lastchar == " " || r == 0)
            {
                m_TextField.text += m_Char;
            }
            else
            {
                m_TextField.text += " ";
            }
        }
    }

    public void NotifSound()
    {
        if(GameManager.m_instance.m_Player.m_Looking == Player.View.Phone)
        {
            m_NotifSource.clip = m_NotificationSFX;
        }
        else
        {
            m_NotifSource.clip = m_VibrateSFX;
        }
        if(GameManager.m_instance.m_GameLost == false && GameManager.m_instance.m_DisableInput == false)
        {
            m_NotifSource.Play();
        }
    }

    public void TypeSound()
    {
        m_TypingSource.clip = m_TypingSFX;
        m_TypingSource.Play();
    }
}
