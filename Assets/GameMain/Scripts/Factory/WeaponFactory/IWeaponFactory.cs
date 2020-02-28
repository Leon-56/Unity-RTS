using RTS.Weapon;

namespace RTS.Factory
{
    public abstract class IWeaponFactory
    {
        public abstract IWeapon CreateWeapon(ENUM_Weapon enumWeapon);
    }
}