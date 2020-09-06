using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameClearTextViewer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI m_cleartext = null;

    string cleartext = "GameClear!";

    public void ClearTextView()
    {
        m_cleartext.text = cleartext;
        Time.timeScale = 0f;
    }

}
