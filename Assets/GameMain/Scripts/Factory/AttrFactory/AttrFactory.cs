using System.Collections.Generic;
using RTS.GameAttr;
using RTS.Weapon;
using UnityEngine;

namespace RTS.Factory
{
    public class AttrFactory : IAttrFactory
    {
        private Dictionary<int, BaseAttr> m_SoldierAttrDB = null;
        private Dictionary<int, EnemyBaseAttr> m_EnemyAttrDB = null;
        private Dictionary<int, WeaponAttr> m_WeaponAttrDB = null;

        public AttrFactory()
        {
            InitSoldierAttr();
            InitEnemyAttr();
            InitWeaponAttr();
        }

        // 产生所有的Soldier的属性
        private void InitSoldierAttr()
        {
            m_SoldierAttrDB = new Dictionary<int, BaseAttr>();
            // 生命力，移动速度，属性名称
            m_SoldierAttrDB.Add(1, new CharacterBaseAttr(10, 3.0f, "新兵"));
            m_SoldierAttrDB.Add(2, new CharacterBaseAttr(20, 3.2f, "中士"));
            m_SoldierAttrDB.Add(3, new CharacterBaseAttr(30, 3.4f, "上尉"));
            m_SoldierAttrDB.Add(11, new CharacterBaseAttr(3, 0.0f, "勇士"));
        }
        
        // 产生所有的Enemy的属性
        private void InitEnemyAttr()
        {
            m_EnemyAttrDB = new Dictionary<int, EnemyBaseAttr>();
            // 生命力，移动速度，属性名称，暴击率
            m_EnemyAttrDB.Add(1, new EnemyBaseAttr(5, 3.0f, "精灵", 10));
            m_EnemyAttrDB.Add(2, new EnemyBaseAttr(15, 3.1f, "山妖", 20));
            m_EnemyAttrDB.Add(3, new EnemyBaseAttr(20, 3.3f, "怪物", 40));
        }
        
        // 产生所有Weapon的属性
        private void InitWeaponAttr()
        {
            m_WeaponAttrDB = new Dictionary<int, WeaponAttr>();
            // 攻击力，距离，属性名称
            m_WeaponAttrDB.Add(1, new WeaponAttr(2, 4, "短枪"));
            m_WeaponAttrDB.Add(2, new WeaponAttr(4, 7, " 长枪"));
            m_WeaponAttrDB.Add(3, new WeaponAttr(8, 10, "火箭筒"));
        }
        
        // 获得Soldier的属性
        public override SoldierAttr GetSoldierAttr(int AttrID)
        {
            if (m_SoldierAttrDB.ContainsKey(AttrID) == false)
            {
                Debug.LogWarning("GetSoldierAttr:AttrID[" + AttrID + "]属性不存在");
                return null;
            }
            // 产生属性对象并设置共享的属性数据
            SoldierAttr NewAttr = new SoldierAttr();
            NewAttr.SetBaseAttr(m_SoldierAttrDB[AttrID]);
            return NewAttr;
        }
        
        // 获取Enemy的属性，传入外部参数CritRate
        public override EnemyAttr GetEnemyAttr(int AttrID)
        {
            if (m_EnemyAttrDB.ContainsKey(AttrID) == false)
            {
                Debug.LogWarning("GetEnemyAttr:AttrID[" + AttrID + "]属性不存在");
                return null;
            }
            // 产生属性对象并设置共享的属性数据
            EnemyAttr NewAttr = new EnemyAttr();
            NewAttr.SetEnemyAttr(m_EnemyAttrDB[AttrID]);
            return NewAttr;
        }

        // 获取武器的属性
        public override WeaponAttr GetWeaponAttr(int AttrID)
        {
            if (m_WeaponAttrDB.ContainsKey(AttrID) == false)
            {
                Debug.LogWarning("GetWeaponAttr:AttrID[" + AttrID + "]属性不存在");
                return null;
            }
            return m_WeaponAttrDB[AttrID];
        }
    }
}