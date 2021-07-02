using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject m_PauseMenu;
    private AudioSource[] allSources;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.m_instance.m_PauseMenu = m_PauseMenu;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && GameManager.m_instance.m_GameLost == false && GameManager.m_instance.m_EndMan.m_EndingCreated == false)
        {
            GameManager.m_instance.PauseGame(!GameManager.m_instance.m_PauseMenu.activeSelf);
        }
    }
}
