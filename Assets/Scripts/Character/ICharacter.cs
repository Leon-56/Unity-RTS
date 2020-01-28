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

        protected bool m_bKilled = false;
        protected bool m_bCheckKilled = false;
        protected float m_RemoveTimer = 1.5f;
        protected bool m_bCanRemove = false;

        private IWeapon m_Weapon = null;
        
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

        public abstract void Attack(ICharacter Target);
        
        public abstract void UnderAttack( ICharacter Attacker);
        
    }
}