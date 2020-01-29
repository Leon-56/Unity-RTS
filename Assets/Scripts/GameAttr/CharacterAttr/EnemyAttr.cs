namespace RTS.GameAttr
{
    public class EnemyAttr : ICharacterAttr
    {
        protected int m_CritRate = 0;
        
        public EnemyAttr() {}

        public EnemyAttr(int MaxHP, float MoveSpeed, int CritRate, string AttrName)
        {
            m_MaxHP = MaxHP;
            m_NowHP = MaxHP;
            m_MoveSpeed = MoveSpeed;
            m_CritRate = CritRate;
            m_AttrName = AttrName;
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