using RTS.GameSystem;

namespace RTS.Weapon
{
    public class WeaponGun : IWeapon
    {
        public WeaponGun() {}
        
        public override void Fire(ICharacter theTarget)
        {
            ShowShootEffect();
            ShowBulletEffect(theTarget.GetPosition(), 0.03f, 0.2f);
            ShowSoundEffect("GunShot");

            theTarget.UnderAttack(m_WeaponOwner);
        }
    }
}