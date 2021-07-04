using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject m_HelpObject;
    public TMP_Text m_Description;

    void Start()
    {
        AudioListener.pause = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Application has quit.");
        }
    }

    public void StartGame()
    {
        LevelLoader g = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        g.LoadLevel("Game");
    }

    public void DisplayHelpText(string text)
    {
        m_Description.text = text;
        m_HelpObject.SetActive(true);
    }

    public void OpenURL(string urlLink)
    {
        Application.OpenURL(urlLink);
    }
}
