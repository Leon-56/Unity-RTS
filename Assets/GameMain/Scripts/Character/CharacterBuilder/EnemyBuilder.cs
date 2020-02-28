using RTS.Factory;
using RTS.GameAttr;
using RTS.GameSystem.CharacterAI;
using RTS.GameSystem.Enemy;
using RTS.Weapon;
using UnityEngine;

namespace RTS.GameSystem.CharacterBuilder
{
    // 建造Enemy时所需的参数
    public class EnemyBuildParam : ICharacterBuildParam
    {
        public Vector3 AttackPosition = Vector3.zero;
        public EnemyBuildParam() {}
    }
    
    // Enemy各个部位的构建
    public class EnemyBuilder : ICharacterBuilder
    {
        private EnemyBuildParam m_BuildParam = null;
        
        public override void SetBuildParam(ICharacterBuildParam theParam)
        {
            m_BuildParam = theParam as EnemyBuildParam;
        }

        public override void LoadAsset(int GameObjectID)
        {
            IAssetFactory AssetFactory = RTSFactory.GetAssetFactory();
            GameObject EnemyGameObject = AssetFactory.LoadEnemy(m_BuildParam.NewCharacter.GetAssetName());
            EnemyGameObject.transform.position = m_BuildParam.SpawnPosition;
            EnemyGameObject.name = string.Format("Enemy[{0}]", GameObjectID);
            m_BuildParam.NewCharacter.SetGameObject(EnemyGameObject);
        }

        public override void AddOnClickScript() { }

        public override void AddWeapon()
        {
            IWeaponFactory WeaponFactory = RTSFactory.GetWeaponFactory();
            IWeapon Weapon = WeaponFactory.CreateWeapon(m_BuildParam.emWeapon);
            
            m_BuildParam.NewCharacter.SetWeapon(Weapon);
        }

        public override void AddAI()
        {
            EnemyAI theAI = new EnemyAI(m_BuildParam.NewCharacter, m_BuildParam.AttackPosition);
            m_BuildParam.NewCharacter.SetAI(theAI);
        }

        public override void SetCharacterAttr()
        {
            IAttrFactory AttrFactory = RTSFactory.GetAttrFactory();
            EnemyAttr theEnemyAttr = AttrFactory.GetEnemyAttr(
                m_BuildParam.NewCharacter.GetAttrID());
            theEnemyAttr.SetAttrStrategy(new EnemyAttrStrategy());
            m_BuildParam.NewCharacter.SetCharacterAttr(theEnemyAttr);
        }

        public override void AddCharacterSystem(RTSGame RTS)
        {
            RTS.AddEnemy(m_BuildParam.NewCharacter as IEnemy);
        }
    }
}