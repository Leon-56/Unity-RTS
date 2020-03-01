using RTS.GameSystem;

namespace RTS.GameAttr
{
    public abstract class ICharacterAttr
    {
        protected BaseAttr m_BaseAttr = null;
        protected int m_NowHP = 0;
        protected IAttrStrategy m_AttrStrategy = null;

        // 设置基本属性
        public void SetBaseAttr(BaseAttr BaseAttr)
        {
            m_BaseAttr = BaseAttr;
        }
        
        // 获取基本属性
        public BaseAttr GetBaseAttr()
        {
            return m_BaseAttr;
        }
        
        public int GetNowHP()
        {
            return m_NowHP;
        }

        public virtual int GetMaxHP()
        {
            return m_BaseAttr.GetMaxHP();
        }
        
        public void FullNowHP()
        {
            m_NowHP = GetMaxHP();
        }

        public virtual float GetMoveSpeed()
        {
            return m_BaseAttr.GetMoveSpeed();
        }
        
        public virtual string GetAttrName()
        {
            return m_BaseAttr.GetAttrName();
        }

        public void SetAttrStrategy(IAttrStrategy theAttrStrategy)
        {
            m_AttrStrategy = theAttrStrategy;
        }

        public IAttrStrategy GetAttrStrategy()
        {
            return m_AttrStrategy;
        }

        public virtual void InitAttr()
        {
            m_AttrStrategy.InitAttr(this);
            FullNowHP();
        }

        public int GetAtkPlusValue()
        {
            return m_AttrStrategy.GetAtkPlusValue(this);
        }

        public void CalDmgValue(ICharacter Attacker)
        {
            int AtkValue = Attacker.GetAtkValue();

            AtkValue -= m_AttrStrategy.GetDmgDescValue(this);

            m_NowHP -= AtkValue;
        }
    }
}