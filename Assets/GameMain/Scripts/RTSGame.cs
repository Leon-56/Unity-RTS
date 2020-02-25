using RTS.GameEvent;
using RTS.GameSystem;
using RTS.GameSystem.Camp;
using RTS.GameSystem.Character;
using RTS.GameSystem.GameEvent;
using RTS.GameSystem.Soldier;
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
        private AchievementSystem m_AchievementSystem = null;
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
            m_AchievementSystem = new AchievementSystem(this);
        
            // UI
            m_CampInfoUI = new CampInfoUI(this);
            m_GamePauseUI = new GamePauseUI(this);
            m_GameStateInfoUI = new GameStateInfoUI(this);
            m_SoldierInfoUI = new SoldierInfoUI(this);
        }

        // Release RTSGame.
        public void Release() { }

        // Update RTSGame.
        public void Update()
        {
            InputProcess();
            
            // Game System
            m_GameEventSystem.Update();
            m_CampSystem.Update();
            m_StageSystem.Update();
            m_CharacterSystem.Update();
            m_APSystem.Update();
            m_AchievementSystem.Update();
            
            // UI
            m_CampInfoUI.Update();
            m_SoldierInfoUI.Update();
            m_GameStateInfoUI.Update();
            m_GamePauseUI.Update(); 
        }

        // Player Inputs.
        private void InputProcess()
        {
            // Mouse
            if(Input.GetMouseButtonUp(0) == false)
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            foreach (var hit in hits)
            {
                // Camp OnClick or not
                CampOnClick CampClickScript = hit.transform.gameObject.GetComponent<CampOnClick>();
                if (CampClickScript != null)
                {
                    CampClickScript.OnClick();
                    return;
                }
                
                // Character OnClick or not
                SoldierOnClick SoldierClickScript = hit.transform.gameObject.GetComponent<SoldierOnClick>();
                if (SoldierClickScript != null)
                {
                    SoldierClickScript.OnClick();
                    return;
                }
            }
        }

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
        
        public void ShowGameMsg( string Msg)
        {
            m_GameStateInfoUI.ShowMsg( Msg );
        }
        
        public void RegisterGameEvent( ENUM_GameEvent emGameEvent, IGameEventObserver Observer)
        {
            m_GameEventSystem.RegisterObserver( emGameEvent , Observer );
        }
        
        // 通知游戏事件
        public void NotifyGameEvent( ENUM_GameEvent emGameEvent, System.Object Param )
        {
            m_GameEventSystem.NotifySubject( emGameEvent, Param);
        }
        
        public void ShowCampInfo( ICamp Camp )
        {
            m_CampInfoUI.ShowInfo( Camp );
            m_SoldierInfoUI.Hide();
        }

        public void ShowSoldierInfo(ISoldier Soldier)
        {
            m_SoldierInfoUI.ShowInfo( Soldier );
            m_CampInfoUI.Hide();
        }

    }

}