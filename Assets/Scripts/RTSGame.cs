using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RTSGame
{
    // Siglenton
    private static RTSGame _instance;
    public static RTSGame Instance
    {
        get
        {
            if (_instance == null)
                _instance = new RTSGame();
            return _instance;
        }
    }

    // SceneState Controller
    private bool m_bGameOver = false;

    private RTSGame() { }

    // Init RTSGame.
    public void Initinal() { }

    // Release RTSGame.
    public void Release() { }

    // Update RTSGame.
    public void Update() { }

    // Player Inputs.
    private void InputProcess() { }

    // Game State.
    public bool ThisGameIsOver()
    {
        return m_bGameOver;
    }

}
