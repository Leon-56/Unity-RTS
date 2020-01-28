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
        
    }
}