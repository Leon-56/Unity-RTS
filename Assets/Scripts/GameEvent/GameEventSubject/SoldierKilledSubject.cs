using RTS.GameSystem.Soldier;

namespace RTS.GameEvent
{
    public class SoldierKilledSubject : IGameEventSubject
    {
        private	int	m_KilledCount = 0;
        private ISoldier m_Soldier = null;

        public SoldierKilledSubject()
        {}
        
        public ISoldier GetSoldier()
        {
            return m_Soldier;
        }
        
        public int GetKilledCount()
        {
            return m_KilledCount;
        }

        // Fire event
        public override void SetParam( System.Object Param )
        {
            base.SetParam( Param);
            m_Soldier = Param as ISoldier;
            m_KilledCount ++;
            
            Notify();
        }
    }
}