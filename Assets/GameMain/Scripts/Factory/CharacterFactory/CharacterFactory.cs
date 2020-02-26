using RTS.GameSystem.Enemy;
using RTS.GameSystem.Soldier;
using RTS.Weapon;
using UnityEngine;

namespace RTS.Factory
{
    public class CharacterFactory : ICharacterFactory
    {
        public override ISoldier CreateSoldier(ENUM_Soldier enumSoldier, ENUM_Weapon enumWeapon, 
            int Level, Vector3 SpawnPosition, Vector3 AttackPosition)
        {
            // 产生对应得Character
            ISoldier theSoldier = null;
            switch (enumSoldier)
            {
                case ENUM_Soldier.Rookie:
                    theSoldier = new SoldierRookie();
                    break;
                case ENUM_Soldier.Sergeant:
                    theSoldier = new SoldierSergeant();
                    break;
                case ENUM_Soldier.Captain:
                    theSoldier = new SoldierCaptain();
                    break;
                default:
                    Debug.LogWarning("CreateSoldier:无法产生[" + enumSoldier + "]");
                    return null;
            }
            
            // 设置模型
           
            // 加入武器
            
            // 获取Soldier的属性，设置给角色
            
            // 加入AI
            
            // 加入管理器

            return theSoldier;
        }

        public override IEnemy CreateEnemy(ENUM_Enemy enumEnemy, ENUM_Weapon enumWeapon, 
            Vector3 SpawnPostion, Vector3 AttackPostion)
        {
            // 产生对应的Character
            IEnemy theEnemy = null;
            switch (enumEnemy)
            {
                case ENUM_Enemy.Elf:
                    theEnemy = new EnemyElf();
                    break;
                case ENUM_Enemy.Troll:
                    theEnemy = new EnemyTroll();
                    break;
                case ENUM_Enemy.Ogre:
                    theEnemy = new EnemyOgre();
                    break;
                default:
                    Debug.LogWarning("CreateEnemy:无法产生[" + enumEnemy + "]");
                    return null;
            }
            
            // 设置模型
           
            // 加入武器
            
            // 获取Soldier的属性，设置给角色
            
            // 加入AI
            
            // 加入管理器

            return theEnemy;
        }
    }
}