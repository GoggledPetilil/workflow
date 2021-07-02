using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimations : MonoBehaviour
{
    public Animator m_Transition;
    private float m_TransTime = 0.25f;
    public bool m_DisableSound; // Legit just a quick bandaid so Virus doesn't play the sound.

    void Awake()
    {
        m_Transition = GetComponent<Animator>();
    }

    void OnEnable()
    {
        if(!m_DisableSound)
        {
            UniversalManager.m_instance.PlaySound(UniversalManager.m_instance.m_MenuPopUp);
        }
        m_Transition.SetTrigger("Fade_In");
    }

    public void DisableThis()
    {
        StartCoroutine(FadeDisable());
    }

    IEnumerator FadeDisable()
    {
        if(!m_DisableSound)
        {
            UniversalManager.m_instance.PlaySound(UniversalManager.m_instance.m_MenuPopDown);
        }

        m_Transition.SetTrigger("Fade_Out");

        yield return new WaitForSeconds(m_TransTime);

        this.gameObject.SetActive(false);
    }
}
