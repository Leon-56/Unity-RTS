using RTS.Weapon;
using UnityEngine;

namespace RTS.GameSystem.CharacterBuilder
{
    // 构建角色需要的参数
    public abstract class ICharacterBuildParam
    {
        public ENUM_Weapon emWeapon = ENUM_Weapon.Null;
        public ICharacter NewCharacter = null;
        public Vector3 SpawnPosition;
        public int AttrID;
        public string AssetName;
        public string IconSpriteName;
    }
    
    // 接口用来生成ICharacter的各个零部件
    public abstract class ICharacterBuilder
    {
        // 设置构建参数
        public abstract void SetBuildParam(ICharacterBuildParam theParam);
        // 加载Asset中的角色模型
        public abstract void LoadAsset(int GameObjectID);
        // 加入OnClickScript
        public abstract void AddOnClickScript();
        // 加入武器
        public abstract void AddWeapon();
        // 加入AI
        public abstract void AddAI();
        // 设置角色能力
        public abstract void SetCharacterAttr();
        // 加入管理器
        public abstract void AddCharacterSystem(RTSGame RTS);
    }
}