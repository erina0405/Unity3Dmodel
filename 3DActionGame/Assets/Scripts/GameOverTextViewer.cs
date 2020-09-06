using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverTextViewer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI m_gameovertext = null;

    string gameovertext = "GameOver!";

    public void GameOverTextView()
    {
        m_gameovertext.text = gameovertext;
        Time.timeScale = 0f;
    }

    
}
