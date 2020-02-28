using RTS.GameAttr;
using RTS.Weapon;

namespace RTS.Factory
{
    public abstract class IAttrFactory
    {
        // 建立武器
        public abstract IWeapon CreateWeapon( ENUM_Weapon emWeapon);
    }
}