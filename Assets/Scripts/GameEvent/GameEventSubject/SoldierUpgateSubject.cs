using RTS.GameSystem.Soldier;

namespace RTS.GameEvent
{
    public class SoldierUpgateSubject : IGameEventSubject
    {
        private	int	m_UpgateCount = 0;
        private ISoldier m_Soldier = null;

        public SoldierUpgateSubject()
        {}
        
        public int GetUpgateCount()
        {
            return m_UpgateCount;
        }

        // Fire Soldier upgate event
        public override void SetParam( System.Object Param )
        {
            base.SetParam( Param);
            m_Soldier = Param as ISoldier;
            m_UpgateCount++;
            
            Notify();
        }

        public ISoldier GetSoldier()
        {
            return m_Soldier;
        }
    }
}