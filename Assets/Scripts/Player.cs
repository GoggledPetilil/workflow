using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum View
    {
      Phone,
      Computer,
      Boss,
      Ceiling
    }

    [Header("View")]
    public View m_Looking;
    public Camera m_Cam;
    public Sprite m_Icon;

    [Header("View Positions")]
    public Transform m_PhoneView;
    public Transform m_ComView;
    public Transform m_BossView;
    public Transform m_CeilingView;

    // Start is called before the first frame update
    void Start()
    {
      m_Looking = View.Phone;
      ViewMode();
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.W) && GameManager.m_instance.m_DisableInput == false)
      {
        switch (m_Looking)
        {
          case View.Phone:
            m_Looking = View.Computer;
            break;
          case View.Boss:
            m_Looking = View.Ceiling;
            break;
        }
        ViewMode();
      }

      if(Input.GetKeyDown(KeyCode.A) && GameManager.m_instance.m_DisableInput == false)
      {
        switch (m_Looking)
        {
          case View.Phone:
            m_Looking = View.Boss;
            break;
          case View.Computer:
            m_Looking = View.Ceiling;
            break;
        }
        ViewMode();
      }

      if(Input.GetKeyUp(KeyCode.W) && GameManager.m_instance.m_DisableInput == false)
      {
        switch (m_Looking)
        {
          case View.Computer:
            m_Looking = View.Phone;
            break;
          case View.Ceiling:
            m_Looking = View.Boss;
            break;
        }
        ViewMode();
      }

      if(Input.GetKeyUp(KeyCode.A) && GameManager.m_instance.m_DisableInput == false)
      {
        switch (m_Looking)
        {
          case View.Boss:
            m_Looking = View.Phone;
            break;
          case View.Ceiling:
            m_Looking = View.Computer;
            break;
        }
        ViewMode();
      }
    }

    void ViewMode()
    {
      switch (m_Looking)
      {
        case View.Phone:
          m_Cam.transform.position = new Vector3(m_PhoneView.position.x, m_PhoneView.position.y, m_Cam.transform.position.z);
          break;
        case View.Computer:
          m_Cam.transform.position = new Vector3(m_ComView.position.x, m_ComView.position.y, m_Cam.transform.position.z);
          break;
        case View.Boss:
          m_Cam.transform.position = new Vector3(m_BossView.position.x, m_BossView.position.y, m_Cam.transform.position.z);
          break;
        case View.Ceiling:
          m_Cam.transform.position = new Vector3(m_CeilingView.position.x, m_CeilingView.position.y, m_Cam.transform.position.z);
          break;
      }
      GameManager.m_instance.SwitchPlayerView();
    }
}
