using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPresenter : MonoBehaviour
{
    [SerializeField]
    GameOverTextViewer m_gameOverTextViewer = null;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_gameOverTextViewer.GameOverTextView();
        }
    }

}
