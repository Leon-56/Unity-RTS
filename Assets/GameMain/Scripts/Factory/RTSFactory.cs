namespace RTS.Factory
{
    public static class RTSFactory
    {
        private static bool m_bLoadFromResource = true;
        private static ICharacterFactory m_CharacterFactory = null;
        // 其他工厂
        
        // 游戏角色工厂
        public static ICharacterFactory GetCharacterFactory()
        {
            if(m_CharacterFactory == null)
                m_CharacterFactory = new CharacterFactory();
            return m_CharacterFactory;
        }
        
        // 其他工厂实例获取
    }
}