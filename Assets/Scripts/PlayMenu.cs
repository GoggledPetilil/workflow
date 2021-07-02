using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayMenu : MonoBehaviour
{
    public bool m_IsCustomDay;

    [Header("UI Elements")]
    public TMP_Text m_DayText;
    public TMP_Text m_DifficultyText;
    public TMP_Text m_HighScore;
    public TMP_Text m_TypeText;
    public TMP_Text m_WarningText;
    public TMP_Text m_SkipEndingText;

    [Header("Story-Mode UI")]
    public Button m_LeftDayButton;
    public Button m_RightDayButton;

    [Header("Enemy Elements")]
    public Slider m_FriendASlider;
    public Slider m_FriendBSlider;
    public Slider m_FriendCSlider;
    public Slider m_BossSlider;
    public Slider m_VirusSlider;
    public TMP_Text m_FriendAText;
    public TMP_Text m_FriendBText;
    public TMP_Text m_FriendCText;
    public TMP_Text m_BossText;
    public TMP_Text m_VirusText;
    public Toggle m_FanToggle;
    public Toggle m_MusicToggle;

    void InitializeMenu()
    {
        UniversalManager um = UniversalManager.m_instance;

        if(um.m_HoldType == true)
        {
            m_TypeText.text = "Typing: Hold";
        }
        else
        {
            m_TypeText.text = "Typing: Mash";
        }
        if(um.m_DangerIndication == true)
        {
            m_WarningText.text = "Warnings: On";
        }
        else
        {
            m_WarningText.text = "Warnings: Off";
        }
        if(um.m_SkipEnding == true)
        {
            m_SkipEndingText.text = "Endings: Skip";
        }
        else
        {
            m_SkipEndingText.text = "Endings: View";
        }

        // Check if this is the Custom menu.
        // Change values accordingly to fit the menu style.
        if(m_IsCustomDay)
        {
            um.m_Day = 0;
            m_DayText.text = "Custom Shift";
            m_HighScore.text = "High-Score:\n" + um.m_HighScores[0].ToString();
        }
        else
        {
            um.m_Day = 1;
            AdjustDayElements();
        }
        SetPresetDifficulty();
        // Determines wether the elements can be interacted with.
        m_FriendASlider.interactable = m_IsCustomDay;
        m_FriendBSlider.interactable = m_IsCustomDay;
        m_FriendCSlider.interactable = m_IsCustomDay;
        m_BossSlider.interactable = m_IsCustomDay;
        m_VirusSlider.interactable = m_IsCustomDay;
        m_FanToggle.interactable = m_IsCustomDay;
        m_MusicToggle.interactable = m_IsCustomDay;
        m_LeftDayButton.gameObject.SetActive(!m_IsCustomDay);
        m_RightDayButton.gameObject.SetActive(!m_IsCustomDay);
    }

    public void SetCustom(bool state)
    {
        m_IsCustomDay = state;
        InitializeMenu();
    }

    public void AdjustDayCount(int amount)
    {
        UniversalManager.m_instance.m_Day += amount;
        if(UniversalManager.m_instance.m_Day < 1)
        {
            UniversalManager.m_instance.m_Day = 5;
        }
        else if(UniversalManager.m_instance.m_Day > 5)
        {
            UniversalManager.m_instance.m_Day = 1;
        }

        AdjustDayElements();
        SetPresetDifficulty();
    }

    public void SetPresetDifficulty()
    {
        UniversalManager um = UniversalManager.m_instance;
        switch(um.m_Day)
        {
          case(0):
            m_FriendASlider.value = Random.Range(0.0f, 1.0f);
            m_FriendBSlider.value = Random.Range(0.0f, 1.0f);
            m_FriendCSlider.value = Random.Range(0.0f, 1.0f);
            m_BossSlider.value = Random.Range(0.0f, 1.0f);
            m_VirusSlider.value = Random.Range(0.0f, 1.0f);
            m_FanToggle.isOn = false;
            m_MusicToggle.isOn = false;
            break;
          case(1):
            m_FriendASlider.value = 0.4f;
            m_FriendBSlider.value = 0.0f;
            m_FriendCSlider.value = 0.0f;
            m_BossSlider.value = 0.5f;
            m_VirusSlider.value = 0.0f;
            m_FanToggle.isOn = false;
            m_MusicToggle.isOn = false;
            break;
          case(2):
            m_FriendASlider.value = 0.5f;
            m_FriendBSlider.value = 0.2f;
            m_FriendCSlider.value = 0.0f;
            m_BossSlider.value = 0.6f;
            m_VirusSlider.value = 0.0f;
            m_FanToggle.isOn = true;
            m_MusicToggle.isOn = false;
            break;
          case(3):
            m_FriendASlider.value = 0.6f;
            m_FriendBSlider.value = 0.4f;
            m_FriendCSlider.value = 0.5f;
            m_BossSlider.value = 0.7f;
            m_VirusSlider.value = 0.2f;
            m_FanToggle.isOn = true;
            m_MusicToggle.isOn = false;
            break;
          case(4):
            m_FriendASlider.value = 0.7f;
            m_FriendBSlider.value = 0.7f;
            m_FriendCSlider.value = 0.7f;
            m_BossSlider.value = 0.8f;
            m_VirusSlider.value = 0.5f;
            m_FanToggle.isOn = true;
            m_MusicToggle.isOn = true;
            break;
          case(5):
            m_FriendASlider.value = 0.9f;
            m_FriendBSlider.value = 0.8f;
            m_FriendCSlider.value = 0.8f;
            m_BossSlider.value = 0.9f;
            m_VirusSlider.value = 0.8f;
            m_FanToggle.isOn = true;
            m_MusicToggle.isOn = true;
            break;
        }
      AdjustDifficultyElements();
    }

    public void AdjustDayElements()
    {
      UniversalManager um = UniversalManager.m_instance;
      m_DayText.text = "Day " + um.m_Day.ToString();
      m_HighScore.text = "High-Score:\n" + um.m_HighScores[um.m_Day].ToString();
    }

    public void AdjustDifficultyElements()
    {
        UniversalManager um = UniversalManager.m_instance;
        // Telling Manager what the values are.
        um.m_FriendAActivity = m_FriendASlider.value;
        um.m_FriendBActivity = m_FriendBSlider.value;
        um.m_FriendCActivity = m_FriendCSlider.value;
        um.m_BossAggression = m_BossSlider.value;
        um.m_VirusAggression = m_VirusSlider.value;
        um.m_FanOn = m_FanToggle.isOn;
        um.m_MusicOn = m_MusicToggle.isOn;

        // Showing values to Player.
        m_FriendAText.text = (m_FriendASlider.value * 100).ToString("F0");
        m_FriendBText.text = (m_FriendBSlider.value * 100).ToString("F0");
        m_FriendCText.text = (m_FriendCSlider.value * 100).ToString("F0");
        m_BossText.text = (m_BossSlider.value * 100).ToString("F0");
        m_VirusText.text = (m_VirusSlider.value * 100).ToString("F0");

        // Calculating the difficulty and showing it.
        string diffText = "";
        float diffVal = m_FriendASlider.value + m_FriendBSlider.value + m_FriendCSlider.value + m_BossSlider.value + m_VirusSlider.value;
        if(um.m_MusicOn)
        {
            diffVal += 0.05f;
        }

        if(diffVal != 0.0)
        {
            if(um.m_FanOn)
            {
                diffVal += 0.05f;
            }
        }
        diffVal = diffVal / 5f;

        if(diffVal <= 0.0)
        {
            diffText = "You're supposed to be at home.";
        }
        else if(diffVal > 0.0 && diffVal <= 0.1)
        {
            diffText = "Forgotten";
        }
        else if(diffVal > 0 && diffVal <= 0.1)
        {
            diffText = "Forgotten";
        }
        else if(diffVal > 0.1 && diffVal <= 0.2)
        {
            diffText = "Mundane";
        }
        else if(diffVal > 0.2 && diffVal <= 0.3)
        {
            diffText = "Chill";
        }
        else if(diffVal > 0.3 && diffVal <= 0.4)
        {
            diffText = "No Problem Here";
        }
        else if(diffVal > 0.4 && diffVal <= 0.5)
        {
            diffText = "Under Control";
        }
        else if(diffVal > 0.5 && diffVal <= 0.6)
        {
            diffText = "Oh.";
        }
        else if(diffVal > 0.6 && diffVal <= 0.7)
        {
            diffText = "Hard at Work";
        }
        else if(diffVal > 0.7 && diffVal <= 0.8)
        {
            diffText = "Heating Up";
        }
        else if(diffVal > 0.8 && diffVal <= 0.9)
        {
            diffText = "Stressing Out";
        }
        else if(diffVal > 0.9 && diffVal <= 1.0)
        {
            diffText = "Crunch Time";
        }
        else if(diffVal >= 1.0)
        {
            if(m_FanToggle.isOn == true && m_MusicToggle.isOn == true)
            {
                diffText = "LEAVE ME ALONE";
            }
            else
            {
                diffText = "Crunch Time";
            }
        }
        // I know, this killed me too.
        m_DifficultyText.text = diffText;
    }
}
