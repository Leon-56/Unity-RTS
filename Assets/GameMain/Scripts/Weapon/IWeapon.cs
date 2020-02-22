using RTS.GameSystem;
using UnityEngine;

namespace RTS.Weapon
{
    public abstract class IWeapon
    {
        protected int m_AtkPlusValue = 0;
        protected int m_Atk = 0;
        protected float m_Range = 0.0f;

        protected GameObject m_GameObject = null;
        protected ICharacter m_WeaponOwner = null;

        protected float m_EffectDisplayTime = 0;
        protected ParticleSystem m_Particles;
        protected LineRenderer m_Line;
        protected AudioSource m_Audio;
        protected Light m_Light;

        protected void ShowBulletEffect(Vector3 TargetPosition, float LineWidth, float DisplayTime)
        {
            if(m_Line == null)
                return;

            m_Line.enabled = true;
            m_Line.startWidth = LineWidth;
            m_Line.endWidth = LineWidth;
            m_Line.SetPosition(0, m_GameObject.transform.position);
            m_Line.SetPosition(1, TargetPosition);
            m_EffectDisplayTime = DisplayTime;
        }

        protected void ShowShootEffect()
        {
            if (m_Particles != null)
            {
                m_Particles.Stop();
                m_Particles.Play();
            }

            if (m_Light != null)
                m_Line.enabled = true;
        }

        protected void ShowSoundEffect(string ClipName)
        {
            if(m_Audio == null)
                return;
            
            
        }

        public void Release()
        {
            if( m_GameObject != null)
                GameObject.Destroy( m_GameObject);
        }
        
        public void SetOwner( ICharacter Owner )
        {
            m_WeaponOwner = Owner;
        }

        public void SetAtkPlusValue(int Value)
        {
            m_AtkPlusValue = Value;
        }

        public GameObject GetGameObject()
        {
            return m_GameObject;
        }

        public int GetAtkValue()
        {
            return m_Atk + m_AtkPlusValue;
        }

        public float GetAtkRange()
        {
            return 0;
        }

        public abstract void Fire(ICharacter theTarget);
    }
}