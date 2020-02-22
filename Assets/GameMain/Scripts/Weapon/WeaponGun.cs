using RTS.GameSystem;

namespace RTS.Weapon
{
    public class WeaponGun : IWeapon
    {
        public WeaponGun() {}

        protected override void DoShowBulletEffect(ICharacter theTarget)
        {
            ShowBulletEffect(theTarget.GetPosition(), 0.03f, 0.2f);
        }

        protected override void DoShowSoundEffect()
        {
            ShowSoundEffect("GunShot");
        }
    }
}