using RTS.GameSystem;

namespace RTS.GameEvent
{
    public class EnemyKilledObserverAchievement : IGameEventObserver
    {
        private EnemyKilledSubject m_Subject = null;
        private  AchievementSystem m_AchievementSystem = null;

        public EnemyKilledObserverAchievement( AchievementSystem theAchievementSystem)
        {
            m_AchievementSystem = theAchievementSystem;
        }
        
        public override void Update()
        {
            m_AchievementSystem.AddEnemyKilledCount();
        }

        public override void SetSubject(IGameEventSubject Subject)
        {
            m_Subject = Subject as EnemyKilledSubject;
        }
    }
}