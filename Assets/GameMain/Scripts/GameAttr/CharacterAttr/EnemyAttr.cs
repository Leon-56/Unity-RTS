namespace RTS.GameAttr
{
    public class EnemyAttr : ICharacterAttr
    {
        protected int m_CritRate = 0;
        
        public EnemyAttr() {}

        public void SetEnemyAttr(BaseAttr baseAttr)
        {
            base.SetBaseAttr(baseAttr);
            m_CritRate = 0;
        }

        public int GetCritRate()
        {
            return m_CritRate;
        }

        public void CutdownCritRate()
        {
            m_CritRate -= m_CritRate / 2;
        }
    }
}