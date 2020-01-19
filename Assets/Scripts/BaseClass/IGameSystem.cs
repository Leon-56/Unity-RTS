using UnityEngine;
using System.Collections;
using RTS;

namespace RTS.GameSystem
{
    public abstract class IGameSystem
    {
        protected RTSGame m_RTSGame = null;

        public IGameSystem(RTSGame RTS)
        {
            m_RTSGame = RTS;
        }
    
        public virtual void Initialize(){ }
        public virtual void Relese() { }
        public virtual void Update() { }
    
    }
}