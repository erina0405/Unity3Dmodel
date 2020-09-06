using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPresenter : MonoBehaviour
{
    [SerializeField]
    GameOverTextViewer m_gameOverTextViewer = null;

    private void OnTriggerEnter(Collider other)
    {
        m_gameOverTextViewer.GameOverTextView();
    }

}
