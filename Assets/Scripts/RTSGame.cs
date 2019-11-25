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

    // Game System.
    private GameEventSystem m_GameEventSystem = null;
    private CampSystem m_CampSystem = null;
    private StageSystem m_StageSystem = null;
    private CharacterSystem m_CharacterSystem = null;
    private APSystem m_APSystem = null;
    private AchievementSystem m_Achievement = null;
    // UI
    private CampInfoUI m_CampInfoUI = null;
    private GamePauseUI m_GamePauseUI = null;
    private GameStateInfoUI m_GameStateInfoUI = null;
    private SoldierInfoUI m_SoldierInfoUI = null;

    private RTSGame() { }

    // Init RTSGame.
    public void Initinal()
    {
        // Scene Control.
        m_bGameOver = false;
        // Game System.
        m_GameEventSystem = new GameEventSystem();
        m_CampSystem = new CampSystem();
        m_StageSystem = new StageSystem();
        m_CharacterSystem = new CharacterSystem();
        m_APSystem = new APSystem();
        m_Achievement = new AchievementSystem();    
    }

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
