using System.Collections.Generic;
using UnityEngine;

namespace RTS.GameSystem.CharacterAI
{
    public class ChaseAIState : IAIState
    {
        private ICharacter m_ChaseTarget = null;  // 追击的目标

        private const float CHASE_CHECK_DIST = 0.2f;
        private Vector3 m_ChasePosition = Vector3.zero;
        private bool m_bOnChase = false;

        public ChaseAIState(ICharacter ChaseTarget)
        {
            m_ChaseTarget = ChaseTarget;
        }
        
        // 更新
        public override void Update(List<ICharacter> Targets)
        {
            // 没有目标时，改为待机
            if (m_ChaseTarget == null || m_ChaseTarget.IsKilled())
            {
                m_CharacterAI.ChangeAIState(new IdleAIState());
                return;
            }
            
            // 在攻击目标内，改为攻击
            if (m_CharacterAI.TargetInAttackRange(m_ChaseTarget))
            {
                m_CharacterAI.StopMove();
                m_CharacterAI.ChangeAIState(new AttackAIState(m_ChaseTarget));
                return;
            }
            
            // 已经在追击
            if (m_bOnChase)
            {
                // 已经到达追击目标，但目标不见，改为待机
                float dist = Vector3.Distance(m_ChasePosition, m_CharacterAI.GetPosition());
                if(dist < CHASE_CHECK_DIST)
                    m_CharacterAI.ChangeAIState(new IdleAIState());
                return;
            }
            
            // 往目标移动
            m_bOnChase = true;
            m_ChasePosition = m_ChaseTarget.GetPosition();
            m_CharacterAI.MoveTo(m_ChasePosition);
        }

        // 目标删除
        public override void RemoveTarget(ICharacter Target)
        {
            if (m_ChaseTarget.GetGameObject().name == Target.GetGameObject().name)
                m_ChaseTarget = null;
        }
    }
}