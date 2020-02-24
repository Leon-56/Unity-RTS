using System.Collections.Generic;
using UnityEngine;

namespace RTS.GameSystem.CharacterAI
{
    public class MoveAIState : IAIState
    {
        private const float MOVE_CHECK_DIST = 1.5f;
        private bool m_bOnMove = false;
        private Vector3 m_AttackPosition = Vector3.zero;
        
        public MoveAIState() { }
        
        // 设置要攻击的目标
        public override void SetAttackPosition(Vector3 AttackPosition)
        {
            m_AttackPosition = AttackPosition;
        }

        // 更新
        public override void Update(List<ICharacter> Targets)
        {
            // 有目标时，改为待机状态
            if (Targets != null || Targets.Count > 0)
            {
                m_CharacterAI.ChangeAIState(new IdleAIState());
                return;
            }
            
            // 已经移动目标
            if (m_bOnMove)
            {
                // 是否到达目标
                float dist = Vector3.Distance(m_AttackPosition, m_CharacterAI.GetPosition());

                if (dist < MOVE_CHECK_DIST)
                {
                    m_CharacterAI.ChangeAIState(new IdleAIState());
                    if (m_CharacterAI.IsKilled() == false)
                        m_CharacterAI.CanAttackHeart(); // 占领阵地
                    m_CharacterAI.Killed();
                }
                return;
            }
            
            // 往目标移动
            m_bOnMove = true;
            m_CharacterAI.MoveTo(m_AttackPosition);
        }
    }
}