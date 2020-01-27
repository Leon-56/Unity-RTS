namespace RTS.GameEvent
{
    public class EnemyKilledObserverUI : IGameEventObserver
    {
        private EnemyKilledSubject m_Subject = null;
        private RTSGame m_RTSGame = null;

        public EnemyKilledObserverUI(RTSGame rtsGame)
        {
            m_RTSGame = rtsGame;
        }
        
        public override void Update()
        {
            m_RTSGame.ShowGameMsg("敌方单位阵亡");
        }

        public override void SetSubject(IGameEventSubject Subject)
        {
            m_Subject = Subject as EnemyKilledSubject;
        }
    }
}