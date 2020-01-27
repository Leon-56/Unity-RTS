using UnityEngine;
using System;
using System.Collections;
using RTS;
using RTS.GameEvent;
using RTS.GameSystem.GameEvent;

namespace RTS.GameSystem
{
    public class AchievementSystem : IGameSystem
    {
        private int m_EnemyKilledCount = 0;
        private int m_SoldierKilledCount = 0;
        private int m_StageLv = 0;
        
        public AchievementSystem(RTSGame RTS) : base(RTS)
        {
            Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();
            
            m_RTSGame.RegisterGameEvent(ENUM_GameEvent.EnemyKilled, new EnemyKilledObserverAchievement(this));
            m_RTSGame.RegisterGameEvent(ENUM_GameEvent.SoldierKilled, new SoldierKilledObserverAchievement(this));
            m_RTSGame.RegisterGameEvent(ENUM_GameEvent.NewStage, new NewStageObserverAchievement(this));
        }

        public void AddEnemyKilledCount()
        {
            m_EnemyKilledCount++;
        }

        public void AddSoldierKilledCount()
        {
            m_SoldierKilledCount++;
        }

        public void SetNowStageLevel(int NowStageLevel)
        {
            m_StageLv = NowStageLevel;
        }
    }
}