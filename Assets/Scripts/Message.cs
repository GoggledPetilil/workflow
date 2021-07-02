using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour
{
    public Sprite m_Icon;
    public Sprite m_Backdrop;
    public SpriteRenderer m_IconRender;
    public SpriteRenderer m_BGRender;

    public void UpdateGraphics()
    {
        m_IconRender.sprite = m_Icon;
        m_BGRender.sprite = m_Backdrop;
    }
}
