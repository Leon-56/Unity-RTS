namespace RTS.GameSystem.Enemy
{
    public enum ENUM_Enemy
    {
        Null 	= 0,
        Elf		= 1,// 精靈
        Troll	= 2,// 山妖
        Ogre	= 3,// 怪物
        Catpive = 4,// 俘兵
        Max,
    }

// Enemy角色界面
    public abstract class IEnemy : ICharacter
    {
        protected ENUM_Enemy m_emEnemyType = ENUM_Enemy.Null;

        public IEnemy()
        {}

        public ENUM_Enemy GetEnemyType() 
        {
            return m_emEnemyType;
        }

        public override void Attack(ICharacter Target)
        {
            
        }

        public override void UnderAttack(ICharacter Attacker)
        {
            // 计算伤害值
             m_Attribute.CalDmgValue(Attacker);

             DoPlayHitSound();
             DoShowHitEffect();
             
             // 是否阵亡
             if(m_Attribute.GetNowHP() <= 0)
                 Killed();
        }

        // 播放音效
        public abstract void DoPlayHitSound();

        // 播放特效
        public abstract void DoShowHitEffect();
    }
}