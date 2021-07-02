using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public void PlaySound()
    {
        UniversalManager.m_instance.PlaySound(UniversalManager.m_instance.m_Start);
    }
}
