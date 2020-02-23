using System.Collections.Generic;
using UnityEngine;

namespace RTS.GameSystem.CharacterAI
{
    public class IdleAIState : IAIState
    {
        bool m_bSetAttackPosition = false;
        
        public IdleAIState() {}

        // 设置要攻击的目标
        public override void SetAttackPosition(Vector3 AttackPosition)
        {
            m_bSetAttackPosition = true;
        }

        // 更新
        public override void Update(List<ICharacter> Targets)
        {
            // 没有目标时
            if (Targets == null || Targets.Count == 0)
            {
                // 有设置目标时，往目标移动
                if (m_bSetAttackPosition)
                    m_CharacterAI.ChangeAIState(new MoveAIState());
            }
        }
    }
}