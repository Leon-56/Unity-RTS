using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Scene
{
    public class SceneStateController
    {
        private ISceneState m_State;
        private bool m_bRunBegin = false;
        public SceneStateController() { }

        // Set State.
        public void SetState(ISceneState State, string LoadSceneName)
        {
            //Debug.Log("SetState:" + State.ToString());
            m_bRunBegin = false;

            // Load Scene.
            LoadScene(LoadSceneName);

            // End the State before.
            if (m_State != null)
                m_State.StateEnd();

            // Set State.
            m_State = State;
        }

        // Load Scene.
        private void LoadScene(string LoadSceneName)
        {
            if (LoadSceneName == null || LoadSceneName.Length == 0)
                return;
            Application.LoadLevel(LoadSceneName);
        }

        // Update.
        public void StateUpdate()
        {
            // is Loading?
            if (Application.isLoadingLevel)
                return;

            // Notice new State to Begin.
            if(m_State != null && m_bRunBegin == false)
            {
                m_State.StateBegin();
                m_bRunBegin = true;
            }

            if (m_State != null)
                m_State.StateUpdate();
        }

    }

}