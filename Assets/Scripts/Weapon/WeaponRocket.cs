using RTS.GameSystem;

namespace RTS.Weapon
{
    public class WeaponRocket : IWeapon
    {
        public WeaponRocket() {}
        
        public override void Fire(ICharacter theTarget)
        {
            ShowShootEffect();
            ShowBulletEffect(theTarget.GetPosition(), 0.8f, 0.5f);
            ShowSoundEffect("Rocket");
            
            theTarget.UnderAttack(m_WeaponOwner);
        }
    }
}