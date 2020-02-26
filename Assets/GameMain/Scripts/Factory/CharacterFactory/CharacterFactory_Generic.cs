using RTS.GameSystem.Enemy;
using RTS.GameSystem.Soldier;
using RTS.Weapon;
using UnityEngine;

namespace RTS.Factory
{
    public class CharacterFactory_Generic : TCharacterFactory_Generic
    {
        public ISoldier CreateSoldier<T>(ENUM_Weapon enumWeapon, int Lv, Vector3 SpqwPosition) where T : ISoldier, new()
        {
            return null;
        }

        public IEnemy CreateEnemy<T>(ENUM_Weapon enumWeapon, Vector3 SpawnPosition, Vector3 AttackPosition) where T : IEnemy, new()
        {
            return null;
        }
    }
}