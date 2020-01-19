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

    }
}