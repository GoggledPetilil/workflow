using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public Slider m_VolumeSlider;
    public TMP_Text m_VolumeText;
    public Button m_TypeStyleButton;
    public TMP_Text m_TypeStyleText;
    public Button m_WarningButton;
    public TMP_Text m_WarningText;
    public Button m_SkipEndingButton;
    public TMP_Text m_SkipEndingText;

    void Start()
    {
        m_VolumeSlider.value = UniversalManager.m_instance.m_UnimVolume;
        m_VolumeText.text = (m_VolumeSlider.value * 100).ToString("F0") + "%";

        if(UniversalManager.m_instance.m_HoldType == true)
        {
            m_TypeStyleText.text = "Hold";
        }
        else
        {
            m_TypeStyleText.text = "Mash";
        }

        if(UniversalManager.m_instance.m_DangerIndication == true)
        {
            m_WarningText.text = "On";
        }
        else
        {
            m_WarningText.text = "Off";
        }

        if(UniversalManager.m_instance.m_SkipEnding == true)
        {
            m_SkipEndingText.text = "On";
        }
        else
        {
            m_SkipEndingText.text = "Off";
        }
    }

    public void AdjustVolume()
    {
        AudioListener.volume = m_VolumeSlider.value;
        UniversalManager.m_instance.m_UnimVolume = m_VolumeSlider.value;
        m_VolumeText.text = (m_VolumeSlider.value * 100).ToString("F0") + "%";
    }

    public void AdjustTypeStyle()
    {
        UniversalManager.m_instance.m_HoldType = !UniversalManager.m_instance.m_HoldType;
        if(UniversalManager.m_instance.m_HoldType == true)
        {
            m_TypeStyleText.text = "Hold";
        }
        else
        {
            m_TypeStyleText.text = "Mash";
        }
    }

    public void AdjustWarningIndication()
    {
        UniversalManager.m_instance.m_DangerIndication = !UniversalManager.m_instance.m_DangerIndication;
        if(UniversalManager.m_instance.m_DangerIndication == true)
        {
            m_WarningText.text = "On";
        }
        else
        {
            m_WarningText.text = "Off";
        }
    }

    public void AdjustSkipEnding()
    {
        UniversalManager.m_instance.m_SkipEnding = !UniversalManager.m_instance.m_SkipEnding;
        if(UniversalManager.m_instance.m_SkipEnding == true)
        {
            m_SkipEndingText.text = "On";
        }
        else
        {
            m_SkipEndingText.text = "Off";
        }
    }
}
