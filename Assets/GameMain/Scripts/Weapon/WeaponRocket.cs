using RTS.GameSystem;

namespace RTS.Weapon
{
    public class WeaponRocket : IWeapon
    {
        public WeaponRocket() {}

        protected override void DoShowBulletEffect(ICharacter theTarget)
        {
            ShowBulletEffect(theTarget.GetPosition(), 0.8f, 0.5f);
        }

        protected override void DoShowSoundEffect()
        {
            ShowSoundEffect("RocketShot");
        }
    }
}