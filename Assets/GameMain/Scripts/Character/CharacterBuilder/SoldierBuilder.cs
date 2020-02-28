using RTS.Factory;
using RTS.GameAttr;
using RTS.GameSystem.Character;
using RTS.GameSystem.CharacterAI;
using RTS.GameSystem.Soldier;
using RTS.Weapon;
using UnityEngine;

namespace RTS.GameSystem.CharacterBuilder
{
    // 构造Soldier时所需要的参数
    public class SoldierBuildParam : ICharacterBuildParam
    {
        public int Lv = 0;
        public SoldierBuildParam() { }
    }
    
    // Soldier各个部位的构建
    public class SoldierBuilder : ICharacterBuilder
    {
        private SoldierBuildParam m_BuildParam = null;
        
        public override void SetBuildParam(ICharacterBuildParam theParam)
        {
            m_BuildParam = theParam as SoldierBuildParam;
        }

        // 加载Asset中的角色模型
        public override void LoadAsset(int GameObjectID)
        {
            IAssetFactory AssetFactory = RTSFactory.GetAssetFactory();
            GameObject SoldierGameObject = AssetFactory.LoadSoldier(
                m_BuildParam.NewCharacter.GetAssetName());
            SoldierGameObject.transform.position = m_BuildParam.SpawnPosition;
            SoldierGameObject.name = string.Format("Soldier[{0}]", GameObjectID);
            m_BuildParam.NewCharacter.SetGameObject(SoldierGameObject);
        }

        // 加入OnClickScript
        public override void AddOnClickScript()
        {
            SoldierOnClick Script = m_BuildParam.NewCharacter.GetGameObject().AddComponent<SoldierOnClick>();
            Script.Solder = m_BuildParam.NewCharacter as ISoldier;
        }

        // 加入武器
        public override void AddWeapon()
        {
            IWeaponFactory WeaponFactory = RTSFactory.GetWeaponFactory();
            IWeapon Weapon = WeaponFactory.CreateWeapon(m_BuildParam.emWeapon);
            
            // 设置给角色
            m_BuildParam.NewCharacter.SetWeapon(Weapon);
        }

        // 设置角色AI
        public override void AddAI()
        {
            SoldierAI theAI = new SoldierAI(m_BuildParam.NewCharacter);
            m_BuildParam.NewCharacter.SetAI(theAI);
        }

        // 设置角色能力
        public override void SetCharacterAttr()
        {
            // 获取Soldier的属性
            IAttrFactory theAttrFactory = RTSFactory.GetAttrFactory();
            SoldierAttr theSoldierAttr = theAttrFactory.GetSoldierAttr(
                m_BuildParam.NewCharacter.GetAttrID());
            
            // 设置
            theSoldierAttr.SetAttrStrategy(new SoldierAttrStrategy());
            
            // 设置等级
            theSoldierAttr.SetSoldierLv(m_BuildParam.Lv);
            
            // 设置给角色
            m_BuildParam.NewCharacter.SetCharacterAttr(theSoldierAttr);
        }

        // 加入管理器
        public override void AddCharacterSystem(RTSGame RTS)
        {
            RTS.AddSoldier(m_BuildParam.NewCharacter as ISoldier);
        }
    }
}