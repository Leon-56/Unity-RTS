using RTS.GameSystem;

namespace RTS.Weapon
{
    public class WeaponRifle : IWeapon
    {
        public WeaponRifle() {}
        
        public override void Fire(ICharacter theTarget)
        {
            ShowShootEffect();
            ShowBulletEffect(theTarget.GetPosition(), 0.5f, 0.2f);
            ShowSoundEffect("RifleShot");
            
            theTarget.UnderAttack(m_WeaponOwner);
        }
    }
}