namespace RTS.Factory
{
    public static class RTSFactory
    {
        private static bool m_bLoadFromResource = true;
        private static ICharacterFactory m_CharacterFactory = null;
        private static IAssetFactory m_AssetFactory = null;
        private static IWeaponFactory m_WeaponFactory = null;
        private static IAttrFactory m_AttrFactory = null;

        // 游戏角色工厂
        public static ICharacterFactory GetCharacterFactory()
        {
            if(m_CharacterFactory == null)
                m_CharacterFactory = new CharacterFactory();
            return m_CharacterFactory;
        }

        public static IAssetFactory GetAssetFactory()
        {
            if (m_AssetFactory == null)
            {
                // to do
            }
            return m_AssetFactory;
        }

        public static IAttrFactory GetAttrFactory()
        {
            if(m_AttrFactory == null)
                m_AttrFactory = new AttrFactory();
            return m_AttrFactory;
        }

        public static IWeaponFactory GetWeaponFactory()
        {
            if(m_WeaponFactory == null)
                m_WeaponFactory = new WeaponFactory();
            return m_WeaponFactory;
        }
    }
}