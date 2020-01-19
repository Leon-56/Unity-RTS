using UnityEngine;
using System.Collections;

namespace RTS.UI
{
    // UI
    public abstract class IUserInterface
    {
        protected RTSGame m_RTSGame = null;
        protected GameObject m_RootUI = null;
        private bool m_bActive = true;

        public IUserInterface(RTSGame RTS)
        {
            m_RTSGame = RTS;
        }

        public bool IsVisible()
        {
            return m_bActive;
        }

        public virtual void Show()
        {
            m_RootUI.SetActive(true);
            m_bActive = true;
        }

        public virtual void Hide()
        {
            m_RootUI.SetActive(false);
            m_bActive = false;
        }
    
        public virtual void Initialize(){}
        public virtual void Release(){}
        public virtual void Update(){}
    }
}