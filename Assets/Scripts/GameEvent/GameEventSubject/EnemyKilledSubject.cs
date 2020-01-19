using RTS.GameSystem.Enemy;

namespace RTS.GameEvent
{
    public class EnemyKilledSubject : IGameEventSubject
    {
        private	int	m_KilledCount = 0;
        private IEnemy m_Enemy = null;

        public EnemyKilledSubject()
        {}
        
        public IEnemy GetEnemy()
        {
            return m_Enemy;
        }
        
        public int GetKilledCount()
        {
            return m_KilledCount;
        }

        // Fire enemy killed event
        public override void SetParam( System.Object Param )
        {
            base.SetParam( Param);
            m_Enemy = Param as IEnemy;
            m_KilledCount ++;

            // fire
            Notify();
        }
    }
}