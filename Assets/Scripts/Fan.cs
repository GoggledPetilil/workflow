using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
  public AudioSource m_Audio;
  public AudioClip m_Clip;
  public Animator m_Anim;

  // Start is called before the first frame update
  void Start()
  {
      if(UniversalManager.m_instance.m_FanOn)
      {
          m_Audio.clip = m_Clip;
          m_Audio.Play();
      }
      else
      {
          m_Anim.enabled = false;
      }
      Destroy(this);
  }
}
