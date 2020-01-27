using RTS.GameSystem;

namespace RTS.GameEvent
{
    public class NewStageObserverAchievement : IGameEventObserver
    {
        private NewStageSubject m_Subject = null;
        private AchievementSystem m_AchievementSystem = null;

        public NewStageObserverAchievement(AchievementSystem theAchievementSystem)
        {
            m_AchievementSystem = theAchievementSystem;
        }
        
        public override void Update()
        {
            m_AchievementSystem.SetNowStageLevel(m_Subject.GetStageCount());
        }

        public override void SetSubject(IGameEventSubject Subject)
        {
           m_Subject = Subject as NewStageSubject;
        }
    }
}