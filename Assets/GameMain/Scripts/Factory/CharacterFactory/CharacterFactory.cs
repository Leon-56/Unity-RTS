using RTS.GameSystem.CharacterBuilder;
using RTS.GameSystem.Enemy;
using RTS.GameSystem.Soldier;
using RTS.Weapon;
using UnityEngine;

namespace RTS.Factory
{
    public class CharacterFactory : ICharacterFactory
    {
        // 角色构建者
        private CharacterBuilderSystem m_BuilderDirector = new CharacterBuilderSystem(RTSGame.Instance);
        
        public override ISoldier CreateSoldier(ENUM_Soldier enumSoldier, ENUM_Weapon enumWeapon, 
            int Level, Vector3 SpawnPosition, Vector3 AttackPosition)
        {
            // 产生Soldier的参数
            SoldierBuildParam SoldierParam = new SoldierBuildParam();
            switch (enumSoldier)
            {
                case ENUM_Soldier.Rookie:
                    SoldierParam.NewCharacter = new SoldierRookie();
                    break;
                case ENUM_Soldier.Sergeant:
                    SoldierParam.NewCharacter = new SoldierSergeant();
                    break;
                case ENUM_Soldier.Captain:
                    SoldierParam.NewCharacter = new SoldierCaptain();
                    break;
                default:
                    Debug.LogWarning("CreateSoldier:无法产生[" + enumSoldier + "]");
                    return null;
            }

            if (SoldierParam.NewCharacter == null)
                return null;
            
            // 设置共享参数
            SoldierParam.emWeapon = enumWeapon;
            SoldierParam.SpawnPosition = SpawnPosition;
            SoldierParam.Lv = Level;
            
            // 产生对应的Builder及设置参数
            SoldierBuilder theSoldierBuilder = new SoldierBuilder();
            theSoldierBuilder.SetBuildParam(SoldierParam);
            
            // 产生
            m_BuilderDirector.Construct(theSoldierBuilder);
            return SoldierParam.NewCharacter as ISoldier;
        }

        public override IEnemy CreateEnemy(ENUM_Enemy enumEnemy, ENUM_Weapon enumWeapon, 
            Vector3 SpawnPostion, Vector3 AttackPostion)
        {
            // 产生Enenmy的参数
            EnemyBuildParam EnemyParam = new EnemyBuildParam();
            switch (enumEnemy)
            {
                case ENUM_Enemy.Elf:
                    EnemyParam.NewCharacter = new EnemyElf();
                    break;
                case ENUM_Enemy.Troll:
                    EnemyParam.NewCharacter = new EnemyTroll();
                    break;
                case ENUM_Enemy.Ogre:
                    EnemyParam.NewCharacter = new EnemyOgre();
                    break;
                default:
                    Debug.LogWarning("CreateEnemy:无法产生[" + enumEnemy + "]");
                    return null;
            }

            if (EnemyParam.NewCharacter == null)
                return null;

            EnemyParam.emWeapon = enumWeapon;
            EnemyParam.SpawnPosition = SpawnPostion;
            EnemyParam.AttackPosition = AttackPostion;
            
            EnemyBuilder theEnemyBuilder = new EnemyBuilder();
            theEnemyBuilder.SetBuildParam(EnemyParam);
            
            m_BuilderDirector.Construct(theEnemyBuilder);
            return EnemyParam.NewCharacter as IEnemy;
        }
    }
}