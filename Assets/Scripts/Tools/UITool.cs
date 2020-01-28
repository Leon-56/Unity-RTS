using UnityEngine;

namespace RTS.Tool
{
    public static class UITool
    {
        private static GameObject m_CanvasObj = null;

        public static void Release()
        {
            m_CanvasObj = null;
        }

        // Find UI from this Canvas.
        public static GameObject FindUIGameObject(string UIName)
        {
            if (m_CanvasObj == null)
                m_CanvasObj = UnityTool.FindGameObject("Canvas");
            if (m_CanvasObj == null)
                return null;
            return UnityTool.FindChildGameObject(m_CanvasObj, UIName);
        }

        // Get UI Element.
        public static T GetUIComponent<T>(GameObject Container, string UIName) where T : UnityEngine.Component
        {
            // Find ChildGameObject.
            GameObject ChildGameObject = UnityTool.FindChildGameObject(Container, UIName);
            if (ChildGameObject == null)
                return null;

            T tempObj = ChildGameObject.GetComponent<T>();
            if(tempObj == null)
            {
                Debug.LogWarning("Element[" + UIName + "]is not[" + typeof(T) + "]");
                return null;
            }

            return tempObj;
        }

        public static T GetUIComponent<T>(string UIName) where T : UnityEngine.Component
        {
            // Get Canvas
            GameObject UIRoot = GameObject.Find("Canvas");
            if (UIRoot == null)
            {
                Debug.LogWarning("Scene hasn't UI Canvas");
                return null;
            }
            return GetUIComponent<T>(UIRoot, UIName);
        }

    }
}