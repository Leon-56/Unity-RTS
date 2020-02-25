using System.Collections.Generic;
using RTS.GameSystem.Enemy;
using RTS.GameSystem.GameEvent;
using RTS.GameSystem.Soldier;

namespace RTS.GameSystem
{
    public class CharacterSystem : IGameSystem
    {
        private List<ICharacter> m_Soldiers = new List<ICharacter>();
        private List<ICharacter> m_Enemies = new List<ICharacter>();
        
        public CharacterSystem(RTSGame RTS) : base(RTS) { }

        // 管理角色
        // 添加Soldier
        public void AddSoldier(ISoldier theSoldier)
        {
            m_Soldiers.Add(theSoldier);
        }
        
        // 删除Soldier
        public void RemoveSoldier(ISoldier theSoldier)
        {
            m_Soldiers.Remove(theSoldier);
        }
        
        // 增加Enemy
        public void AddEnemy(IEnemy theEnemy)
        {
            m_Enemies.Add(theEnemy);
        }
        
        // 删除Enemey
        public void Remove(IEnemy theEnemy)
        {
            m_Enemies.Remove(theEnemy);
        }
        
        // 删除角色
        public void RemoveCharacter()
        {
            RemoveCharacter(m_Soldiers, m_Enemies, ENUM_GameEvent.SoldierKilled);
            RemoveCharacter(m_Enemies, m_Soldiers, ENUM_GameEvent.EnemyKilled);
        }
        
        // 删除角色
        public void RemoveCharacter(List<ICharacter> Characters, List<ICharacter> Opponents, ENUM_GameEvent emEvent)
        {
            // 分别获取可以删除的角色
            List<ICharacter> CanRemoves = new List<ICharacter>();
            foreach (var character in Characters)
            {
                // 是否阵亡
                if (character.IsKilled() == false)
                    continue;
                // 是否确认过阵亡事件
                if (character.CheckKilledEvent() == false)
                    m_RTSGame.NotifyGameEvent(emEvent, character);
                // 是否可以删除
                if(character.CanRemove())
                    CanRemoves.Add(character);
            }
            
            // 删除
            foreach (var canRemove in CanRemoves)
            {
                // 通知对手删除
                foreach (var opponent in Opponents)
                {
                    opponent.RemoveAITarget(canRemove);
                }
                
                // 释放资源
                canRemove.Release();
                Characters.Remove(canRemove);
            }
        }
        
        // Enemy数量
        public int GetEnemyCount()
        {
            return 0;
        }
        
        // 系统定期更新
        // 更新
        public override void Update()
        {
            UpdateCharacter();
            UpdateAI();  // 更新AI
        }
        
        // 更新角色
        public void UpdateCharacter()
        {
            foreach (var character in m_Soldiers)
            {
                character.Update();
            }

            foreach (var character in m_Enemies)
            {
                character.Update();
            }
        }
        
        // 更新AI
        public void UpdateAI()
        {
            // 分别更新两个群组的AI
            UpdateAI(m_Soldiers, m_Enemies);
            UpdateAI(m_Enemies, m_Soldiers);
        }

        public void UpdateAI(List<ICharacter> Characters, List<ICharacter> Targets)
        {
            foreach (var character in Characters)
            {
                character.UpdateAI(Targets);
            }
        }
    }
}