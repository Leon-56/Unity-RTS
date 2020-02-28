using RTS.GameAttr;
using RTS.Weapon;

namespace RTS.Factory
{
    public class AttrFactory : IAttrFactory
    {
        public override IWeapon CreateWeaponAttr(ENUM_Weapon emWeapon)
        {
            return null;
        }

        public override SoldierAttr GetSoldierAttr(int AttrID)
        {
            return null;
        }

        public override EnemyAttr GetEnemyAttr(int AttrID)
        {
            return null;
        }
    }
}