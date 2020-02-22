namespace RTS.GameSystem.Soldier
{
    // Soldier types
    public enum ENUM_Soldier
    {
        Null = 0,
        Rookie	= 1,	// Level1 Rookie
        Sergeant= 2,	// Level2 Sergeant
        Captain = 3,	// Level3 Captain
        Captive	= 4, 	// Level4 Captive
        Max,
    }
    
    public abstract class ISoldier : ICharacter
    {
        protected ENUM_Soldier m_emSoldier = ENUM_Soldier.Null;
        protected int		   m_MedalCount	= 0;
        protected const int	   MAX_MEDAL = 3;
        protected const int    MEDAL_VALUE_ID = 20;

        public ISoldier()
        {
        }

        public ENUM_Soldier GetSoldierType()
        {
            return m_emSoldier;
        }

        public override void Attack(ICharacter Target)
        {
            WeaponAttackTarget(Target);
        }

        public override void UnderAttack(ICharacter Attacker)
        {
            // 计算伤害数值
            m_Attribute.CalDmgValue(Attacker);
            
            // 是否阵亡
            if (m_Attribute.GetNowHP() <= 0)
            {
                DoPlayKilledSound();        // 音效
                DoShowKilledEffect();        // 特效
                Killed();                    // 阵亡
            }
        }

        // 播放音效
        public abstract void DoPlayKilledSound();

        // 播放特效
        public abstract void DoShowKilledEffect();
    }
}