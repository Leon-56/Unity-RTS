using System.Collections.Generic;
using UnityEngine;

namespace RTS.GameSystem.CharacterAI
{
    public abstract class IAIState
    {
        protected ICharacterAI m_CharacterAI = null;  // 角色AI(状态的拥有者)
        
        public IAIState() { }
        
        // 设置CharacterAI的对象
        public void SetCharacterAI(ICharacterAI CharacterAI)
        {
            m_CharacterAI = CharacterAI;
        }
        
        // 设置要攻击的目标
        public virtual void SetAttackPosition(Vector3 AttackPosition) {}
        
        // 更新
        public abstract void Update(List<ICharacter> Targets);
        
        // 目标被删除
        public virtual void RemoveTarget(ICharacter Target) { }
    }
}