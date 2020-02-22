using RTS.GameSystem;

namespace RTS.GameAttr
{
    public abstract class ICharacterAttr
    {
        protected int m_MaxHP = 0;
        protected int m_NowHP = 0;
        protected float m_MoveSpeed = 1.0f;
        protected string m_AttrName = "";

        protected IAttrStrategy m_AttrStrategy = null;

        public int GetNowHP()
        {
            return m_NowHP;
        }

        public virtual int GetMaxHP()
        {
            return m_MaxHP;
        }
        
        public void FullNowHP()
        {
            m_NowHP = GetMaxHP();
        }

        public virtual float GetMoveSpeed()
        {
            return m_MoveSpeed;
        }
        
        public virtual string GetAttrName()
        {
            return m_AttrName;
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