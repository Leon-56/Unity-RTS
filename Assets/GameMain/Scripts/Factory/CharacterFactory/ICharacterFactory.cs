using RTS.GameSystem.Enemy;
using RTS.GameSystem.Soldier;
using RTS.Weapon;
using UnityEngine;

namespace RTS.Factory
{
    public abstract class ICharacterFactory
    {
        // 产生Soldier
        public abstract ISoldier CreateSoldier(ENUM_Soldier enumSoldier, ENUM_Weapon enumWeapon,
            int Level, Vector3 SpawnPosition, Vector3 AttackPosition);

        // 产生Enemy
        public abstract IEnemy CreateEnemy(ENUM_Enemy enumEnemy, ENUM_Weapon enumWeapon,
            Vector3 SpawnPostion, Vector3 AttackPostion);
    }
}