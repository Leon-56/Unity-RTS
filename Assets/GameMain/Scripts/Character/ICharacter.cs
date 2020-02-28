using System.Collections.Generic;
using RTS.GameAttr;
using RTS.GameSystem.CharacterAI;
using RTS.Tool;
using RTS.Weapon;
using UnityEngine;
using UnityEngine.AI;

namespace RTS.GameSystem
{
    public abstract class ICharacter
    {
        protected string m_Name = "";
        protected GameObject m_GameObject = null;
        protected NavMeshAgent m_NavmeshAgent = null;
        protected AudioSource m_Audio = null;
        protected string m_IconSpriteName = "";
        protected string m_AssetName = "";
        protected int m_AttrID;

        protected bool m_bKilled = false;
        protected bool m_bCheckKilled = false;
        protected float m_RemoveTimer = 1.5f;
        protected bool m_bCanRemove = false;

        private IWeapon m_Weapon = null;
        protected ICharacterAttr m_Attribute = null;

        protected ICharacterAI m_AI = null;
        
        public ICharacter(){ }

        public void SetGameObject(GameObject theGameObject)
        {
            m_GameObject = theGameObject;
            m_NavmeshAgent = m_GameObject.GetComponent<NavMeshAgent>();
            m_Audio = m_GameObject.GetComponent<AudioSource>();
        }

        public GameObject GetGameObject()
        {
            return m_GameObject;
        }

        public void Release()
        {
            if (m_GameObject != null)
            {
                GameObject.Destroy(m_GameObject);
            }
        }
        
        // 设置AI
        public void SetAI(ICharacterAI CharacterAI)
        {
            m_AI = CharacterAI;
        }
        
        // 更新AI
        public void UpdateAI(List<ICharacter> Targets)
        {
            m_AI.Update(Targets);
        }
        
        // 通知AI有角色被删除
        public void RemoveAITarget(ICharacter Targets)
        {
            m_AI.RemoveAITarget(Targets);
        }

        public int GetAttrID()
        {
            return m_AttrID;
        }

        // 更新
        public void Update()
        {
            if( m_bKilled)
            {
                m_RemoveTimer -= Time.deltaTime;
                if( m_RemoveTimer <=0 )
                    m_bCanRemove = true;
            }
		
            m_Weapon.Update();
        }

        public string GetName()
        {
            return m_Name;
        }

        public void SetIconSpriteName(string SpriteName)
        {
            m_IconSpriteName = SpriteName;
        }

        public string GetIconSpriteName()
        {
            return m_IconSpriteName;
        }
        
        // 取得Asset名字
        public string GetAssetName()
        {
            return m_AssetName;
        }
        
        public Vector3 GetPosition()
        {
            return m_GameObject.transform.position;
        }

        public void SetWeapon(IWeapon Weapon)
        {
            if (m_Weapon != null)
                m_Weapon.Release();
            m_Weapon = Weapon;

            m_Weapon.SetOwner(this);

            UnityTool.Attach(m_GameObject, m_Weapon.GetGameObject(), Vector3.zero);
        }

        public IWeapon GetWeapon()
        {
            return m_Weapon;
        }

        public virtual void SetCharacterAttr(ICharacterAttr CharacterAttr)
        {
            m_Attribute = CharacterAttr;
            m_Attribute.InitAttr();

            m_NavmeshAgent.speed = m_Attribute.GetMoveSpeed();

            m_Name = m_Attribute.GetAttrName();
        }
        
        // 移动
        public void MoveTo(Vector3 Position)
        {
            
        }
        
        // 停止移动
        public void StopMove()
        {
            
        }

        public bool IsKilled()
        {
            return m_bKilled;
        }
        
        public void Killed()
        {
            if( m_bKilled == true)
                return;
            m_bKilled = true;
            m_bCheckKilled = false;
        }
        
        // 是否确认阵亡过
        public bool CheckKilledEvent()
        {
            if( m_bCheckKilled)
                return true;
            m_bCheckKilled = true;
            return false;
        }
        
        //  是否可以移除
        public bool CanRemove()
        {
            return m_bCanRemove;
        }

        protected void SetWeaponAtkPlusValue(int Value)
        {
            m_Weapon.SetAtkPlusValue(Value);
        }

        protected void WeaponAttackTarget(ICharacter Target)
        {
            m_Weapon.Fire(Target);
        }

        public int GetAtkValue()
        {
            return m_Weapon.GetAtkValue();
        }

        public float GetAttackRange()
        {
            return m_Weapon.GetAtkRange();
        }
        
        public virtual void Attack(ICharacter Target)
        {
            SetWeaponAtkPlusValue(m_Attribute.GetAtkPlusValue());
            
            WeaponAttackTarget(Target);
        }

        public abstract void UnderAttack(ICharacter Attacker);
    }
}