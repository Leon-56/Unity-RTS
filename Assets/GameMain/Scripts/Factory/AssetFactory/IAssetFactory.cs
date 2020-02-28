using UnityEngine;

namespace RTS.Factory
{
    public abstract class IAssetFactory
    {
        // 生产Soldier
        public abstract GameObject LoadSoldier( string AssetName );

        // 生产Enemy
        public abstract GameObject LoadEnemy( string AssetName );

        // 生产Weapon
        public abstract GameObject LoadWeapon( string AssetName );

        // 生产特效
        public abstract GameObject LoadEffect( string AssetName );

        // 生产AudioClip
        public abstract AudioClip  LoadAudioClip(string ClipName);

        // 生产Sprite
        public abstract Sprite	   LoadSprite(string SpriteName);
    }
}