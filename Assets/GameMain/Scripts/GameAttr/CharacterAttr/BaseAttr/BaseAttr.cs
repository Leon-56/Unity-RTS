namespace RTS.GameAttr
{
    public abstract class BaseAttr
    {			
        public abstract int 	GetMaxHP();
        public abstract float 	GetMoveSpeed();
        public abstract string 	GetAttrName();
    }
    
    public class CharacterBaseAttr : BaseAttr
    {
        private int m_MaxHP;  // 最高HP
        private float m_MoveSpeed;  // 当前移动速度
        private string m_AttrName;  // 属性名称

        public CharacterBaseAttr(int MaxHP, float MoveSpeed, string AttrName)
        {
            m_MaxHP = MaxHP;
            m_MoveSpeed = MoveSpeed;
            m_AttrName = AttrName;
        }


        public override int GetMaxHP()
        {
            return m_MaxHP;
        }

        public override float GetMoveSpeed()
        {
            return m_MoveSpeed;
        }

        public override string GetAttrName()
        {
            return m_AttrName;
        }
    }

    // 敌人的属性
    public class EnemyBaseAttr : CharacterBaseAttr
    {
        public int m_CritRate;  // 暴击率
        
        public EnemyBaseAttr(int MaxHP, float MoveSpeed, string AttrName, int CritRate) : base(MaxHP, MoveSpeed, AttrName)
        {
            m_CritRate = CritRate;
        }

        public virtual int GetCritRate()
        {
            return m_CritRate;
        }
    }
}