namespace RTS.GameAttr
{
    public class SoldierAttr : ICharacterAttr
    {
        protected int m_SoldierLv = 0;
        protected int m_AddMaxHP;
        
        public SoldierAttr() {}

        public SoldierAttr(int MaxHP, float MoveSpeed, string AttrName)
        {
            m_MaxHP = MaxHP;
            m_NowHP = MaxHP;
            m_MoveSpeed = MoveSpeed;
            m_AttrName = AttrName;
        }

        public void SetSoldierLv(int Lv)
        {
            m_SoldierLv = Lv;
        }

        public int GetSoldierLv()
        {
            return m_SoldierLv;
        }

        public void AddMaxHP(int AddMaxHP)
        {
            m_AddMaxHP = AddMaxHP;
        }

        public override int GetMaxHP()
        {
            return base.GetMaxHP() + m_AddMaxHP;
        }
    }
}