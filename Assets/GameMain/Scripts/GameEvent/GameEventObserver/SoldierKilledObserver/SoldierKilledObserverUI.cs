using RTS.UI;

namespace RTS.GameEvent
{
    public class SoldierKilledObserverUI : IGameEventObserver
    {
        private SoldierKilledSubject m_Subject = null;
        private SoldierInfoUI m_InfoUI = null;

        public SoldierKilledObserverUI(SoldierInfoUI InfoUI)
        {
            m_InfoUI = InfoUI;
        }
        
        public override void Update()
        {
            m_InfoUI.RefreshSoldier(m_Subject.GetSoldier());
        }

        public override void SetSubject(IGameEventSubject Subject)
        {
            m_Subject = Subject as SoldierKilledSubject;
        }
    }
}