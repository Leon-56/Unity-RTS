using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

    }

}
