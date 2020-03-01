using RTS.GameAttr;
using RTS.Weapon;
using UnityEngine;

namespace RTS.Factory
{
    public class WeaponFactory : IWeaponFactory
    {
        public WeaponFactory() { }
        
        // 建立武器
        public override IWeapon CreateWeapon(ENUM_Weapon enumWeapon)
        {
            IWeapon weapon = null;
            string AssetName = ""; // Unity模型名称
            int AttrID = 0; // 武器能力值

            switch (enumWeapon)
            {
                case ENUM_Weapon.Gun:
                    weapon = new WeaponGun();
                    AssetName = "WeaponGun";
                    AttrID = 1;
                    break;
                case ENUM_Weapon.Rifle:
                    weapon = new WeaponRifle();
                    AssetName = "WeaponRifle";
                    AttrID = 2;
                    break;
                case ENUM_Weapon.Rocket:
                    weapon = new WeaponRocket();
                    AssetName = "WeaponRocket";
                    AttrID = 3;
                    break;
                default:
                    Debug.LogWarning("CreateWeapon无法建立[" + enumWeapon + "]");
                    return null;
            }
            
            // 产生Asset
            IAssetFactory AssetFactory = RTSFactory.GetAssetFactory();
            GameObject WeaponGameObject = AssetFactory.LoadWeapon(AssetName);
            weapon.SetGameObject(WeaponGameObject);
            
            // 获取武器威力
            IAttrFactory AttrFactory = RTSFactory.GetAttrFactory();
            WeaponAttr theWeaponAttr = AttrFactory.GetWeaponAttr(AttrID);
            
            // 设置属性
            weapon.SetWeaponAttr(theWeaponAttr);

            return weapon;
        }
    }
}