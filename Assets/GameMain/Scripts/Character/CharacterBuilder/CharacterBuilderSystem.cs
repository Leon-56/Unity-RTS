namespace RTS.GameSystem.CharacterBuilder
{
    public class CharacterBuilderSystem : IGameSystem
    {
        private int m_GameObjectID = 0;
        
        public CharacterBuilderSystem(RTSGame RTS) : base(RTS)
        {
            
        }
        
        // 构建
        public void Construct(ICharacterBuilder theBuilder)
        {
            // 利用Builder产生的各个部分加入Product中
            theBuilder.LoadAsset(++m_GameObjectID);
            theBuilder.AddOnClickScript();
            theBuilder.AddWeapon();
            theBuilder.SetCharacterAttr();
            theBuilder.AddAI();
            
            // 加入管理器
            theBuilder.AddCharacterSystem(m_RTSGame);
        }
    }
}