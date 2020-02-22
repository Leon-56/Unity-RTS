using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Scene
{
    public class ISceneState
    {
        // State name.
        private string m_StateName = "ISceneState";
        public string StateName
        {
            get
            {
                return m_StateName;
            }
            set
            {
                m_StateName = value;
            }
        }

        // Controller.
        protected SceneStateController m_Controller = null;

        public ISceneState(SceneStateController Controller)
        {
            m_Controller = Controller;
        }

        // Start.
        public virtual void StateBegin() { }

        // End.
        public virtual void StateEnd() { }

        // Update.
        public virtual void StateUpdate() { }

        public override string ToString()
        {
            return string.Format("[I_SceneState: StateName = {0}]", StateName);
        }

    }

}