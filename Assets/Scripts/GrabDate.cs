using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrabDate : MonoBehaviour
{
    public TMP_Text m_Text;

    // Start is called before the first frame update
    void Start()
    {
        string day = System.DateTime.Now.DayOfWeek.ToString() + System.DateTime.Now.ToString(" d MMM");
        m_Text.text = day;
    }
}
