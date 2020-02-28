using RTS.GameAttr;
using RTS.Weapon;

namespace RTS.Factory
{
    public abstract class IAttrFactory
    {
        // 获取武器属性
        public abstract IWeapon CreateWeaponAttr( ENUM_Weapon emWeapon);

        // 获取Soldier属性
        public abstract SoldierAttr GetSoldierAttr(int AttrID);
        
        // 获取Enemy属性
        public abstract EnemyAttr GetEnemyAttr(int AttrID);
    }
}