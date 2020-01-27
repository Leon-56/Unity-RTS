namespace RTS.GameEvent
{
    public class EnemyKilledObserverStageScore : IGameEventObserver
    {
        private EnemyKilledSubject m_Subject = null;
        private StageSystem m_StageSystem = null;

        public EnemyKilledObserverStageScore(StageSystem theStageSystem)
        {
            m_StageSystem = theStageSystem;
        }
        
        public override void Update()
        {
            m_StageSystem.SetEnemyKilledCount(m_Subject.GetKilledCount());
        }

        public override void SetSubject(IGameEventSubject Subject)
        {
            m_Subject = Subject as EnemyKilledSubject;
        }
    }
}