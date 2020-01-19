using System.Collections;
using System.Collections.Generic;
using RTS.GameSystem;
using RTS.GameSystem.GameEvent;
using RTS.UI;
using UnityEngine;

namespace RTS
{
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
            m_GameEventSystem = new GameEventSystem(this);
            m_CampSystem = new CampSystem(this);
            m_StageSystem = new StageSystem(this);
            m_CharacterSystem = new CharacterSystem(this);
            m_APSystem = new APSystem(this);
            m_Achievement = new AchievementSystem(this);
        
            // UI
            m_CampInfoUI = new CampInfoUI(this);
            m_GamePauseUI = new GamePauseUI(this);
            m_GameStateInfoUI = new GameStateInfoUI(this);
            m_SoldierInfoUI = new SoldierInfoUI(this);
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

        public int GetEnemyCount()
        {
            if (m_CharacterSystem != null)
                return m_CharacterSystem.GetEnemyCount();
            return 0;
        }

    }

}