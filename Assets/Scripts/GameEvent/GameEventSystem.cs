using System.Collections;
using System.Collections.Generic;
using RTS.GameEvent;
using RTS.GameSystem;
using UnityEngine;

namespace RTS.GameSystem.GameEvent
{
    // Game Events
    public enum ENUM_GameEvent
    {
        Null = 0,
        EnemyKilled = 1, 
        SoldierKilled = 2,
        SoldierUpgate = 3,
        NewStage = 4,
    }
    
    public class GameEventSystem : IGameSystem
    {
        private Dictionary<ENUM_GameEvent, IGameEventSubject> m_GameEvents
                = new Dictionary<ENUM_GameEvent, IGameEventSubject>();

        public GameEventSystem(RTSGame RTS) : base(RTS)
        {
            Initialize();
        }

        public override void Relese()
        {
            m_GameEvents.Clear();
        }

        public void RegisterObserver(ENUM_GameEvent emGameEvent, IGameEventObserver Observer)
        {
            // Get Subject
            IGameEventSubject Subject = GetGameEventSubject(emGameEvent);
            if (Subject != null)
            {
                Subject.Attach(Observer);
                Observer.SetSubject(Subject);
            }
        }

        private IGameEventSubject GetGameEventSubject(ENUM_GameEvent emGameEvent)
        {
            if (m_GameEvents.ContainsKey(emGameEvent))
                return m_GameEvents[emGameEvent];

            IGameEventSubject rtsSubject = null;
            switch (emGameEvent)
            {
                case ENUM_GameEvent.EnemyKilled:
                    rtsSubject = new EnemyKilledSubject();
                    break;
                case ENUM_GameEvent.SoldierKilled:
                    rtsSubject = new SoldierKilledSubject();
                    break;
                case ENUM_GameEvent.SoldierUpgate:
                    rtsSubject = new SoldierUpgateSubject();
                    break;
                case ENUM_GameEvent.NewStage:
                    rtsSubject = new NewStageSubject();
                    break;
                default:
                    Debug.Log("还没有针对[" + emGameEvent + "]指定要产生的Subject类");
                    return null;
            }
            
            m_GameEvents.Add(emGameEvent, rtsSubject);
            return rtsSubject;
        }

        public void NotifySubject(ENUM_GameEvent emGameEvent, System.Object Param)
        {
            if(m_GameEvents.ContainsKey(emGameEvent) == false)
                return ;
            m_GameEvents[emGameEvent].SetParam(Param);
        }
        
    }
}
