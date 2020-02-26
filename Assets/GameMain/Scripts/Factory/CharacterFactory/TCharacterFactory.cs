using RTS.GameSystem.Enemy;
using RTS.GameSystem.Soldier;
using RTS.Weapon;
using UnityEngine;

namespace RTS.Factory
{
    public interface TCharacterFactory_Generic
    {
        ISoldier CreateSoldier<T>(ENUM_Weapon enumWeapon, int Lv, Vector3 SpqwPosition)
            where T : ISoldier, new();

        IEnemy CreateEnemy<T>(ENUM_Weapon enumWeapon, Vector3 SpawnPosition, Vector3 AttackPosition)
            where T : IEnemy, new();
    }
}