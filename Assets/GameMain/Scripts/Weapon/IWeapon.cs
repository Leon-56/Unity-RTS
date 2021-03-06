﻿using RTS.GameAttr;
using RTS.GameSystem;
using RTS.Tool;
using UnityEngine;

namespace RTS.Weapon
{
    public enum ENUM_Weapon
    {
        Null 	= 0,
        Gun 	= 1,
        Rifle	= 2,	
        Rocket	= 3,	
        Max	,
    }
    
    public abstract class IWeapon
    {
        // 属性
        protected int m_AtkPlusValue = 0;
        // protected int m_Atk = 0;
        // protected float m_Range = 0.0f;
        protected WeaponAttr m_WeaponAttr = null;

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

        public void Update()
        {
            
        }
        
        public void SetOwner( ICharacter Owner )
        {
            m_WeaponOwner = Owner;
        }
        
        public void SetWeaponAttr(WeaponAttr theWeaponAttr)
        {
            m_WeaponAttr = theWeaponAttr;
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
            return m_WeaponAttr.Atk + m_AtkPlusValue;
        }

        public float GetAtkRange()
        {
            return m_WeaponAttr.AtkRange;
        }
        
        // 设置Unity物体
        public void SetGameObject( GameObject theGameObject )
        {
            m_GameObject = theGameObject ;

            //  设置特效
            SetupEffect();
        }
        
        // 设置特效
        protected void SetupEffect()
        {
            GameObject EffectObj = UnityTool.FindChildGameObject( m_GameObject ,"Effect");

            // 取得特效使用的元件
            m_Line = EffectObj.GetComponent <LineRenderer> ();
            m_Particles = EffectObj.GetComponent<ParticleSystem> ();
            m_Audio = EffectObj.GetComponent<AudioSource> ();
            m_Light = EffectObj.GetComponent<Light> ();
        }

        public void Fire(ICharacter theTarget)
        {
            // 显示武器发射/枪口特效
            ShowShootEffect();
            
            // 显示子弹特效(子类实现)
            DoShowBulletEffect(theTarget);
            
            // 播放音效(子类实现)
            DoShowSoundEffect();
            
            theTarget.UnderAttack(m_WeaponOwner);
        }
        
        protected abstract void DoShowBulletEffect( ICharacter theTarget );
        
        protected abstract void DoShowSoundEffect();
    }
}