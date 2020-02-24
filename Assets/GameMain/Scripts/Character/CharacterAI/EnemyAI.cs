using UnityEngine;

namespace RTS.GameSystem.CharacterAI
{
    public class EnemyAI : ICharacterAI
    {
        private static StageSystem m_StageSystem = null;
        private Vector3 m_AttackPosition = Vector3.zero;
        
        // 直接将关卡系统注入EnemyAI类使用
        public static void SetStageSystem(StageSystem StageSystem)
        {
            m_StageSystem = StageSystem;
        }
        
        public EnemyAI(ICharacter Character, Vector3 AttackPosition) : base(Character)
        {
            m_AttackPosition = AttackPosition;
            
            // 起始状态
            ChangeAIState(new IdleAIState());
        }
        
        // 更换AI状态
        public override void ChangeAIState(IAIState NewAIState)
        {
            ChangeAIState(NewAIState);
            
            // Enemy的AI要设置攻击目标
            NewAIState.SetAttackPosition(m_AttackPosition);
        }

        // 是否可以攻击Heart
        public override bool CanAttackHeart()
        {
            // 通过之少一个Heart
            m_StageSystem.LostHeart();
            return true;
        }
    }
}