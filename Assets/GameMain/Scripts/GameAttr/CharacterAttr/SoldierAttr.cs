namespace RTS.GameAttr
{
    public class SoldierAttr : ICharacterAttr
    {
        protected int m_SoldierLv = 0;
        protected int m_AddMaxHP;
        
        public SoldierAttr() {}

        public void SetSoldierAttr(BaseAttr baseAttr)
        {
            // 共享组件
            base.SetBaseAttr(baseAttr);
            // 外部参数
            m_SoldierLv = 1;
            m_AddMaxHP = 0;
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