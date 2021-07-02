using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator m_Transition;
    public float m_TransTime = 0.25f;

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(FadeToLevel(sceneName));
    }

    IEnumerator FadeToLevel(string sceneName)
    {
        m_Transition.SetTrigger("Start");

        yield return new WaitForSeconds(m_TransTime);

        SceneManager.LoadScene(sceneName);
    }
}
