using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendButton : MonoBehaviour
{
  void OnMouseOver()
  {
    if(Input.GetMouseButtonUp(0)){
      GameManager.m_instance.m_Social.SendMessage();
    }
  }
}
