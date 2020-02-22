using UnityEngine;

namespace RTS.Tool
{
    public static class UnityTool
    {
        public static GameObject FindGameObject(string GameObjectName)
        {
            GameObject pTmpGameObj = GameObject.Find(GameObjectName);
            if(pTmpGameObj == null)
            {
                Debug.Log("Can not find GameObject[" + GameObjectName + "]");
                return null;
            }
            return pTmpGameObj;
        }

        public static GameObject FindChildGameObject(GameObject Container, string gameobjectName)
        {
            if (Container == null)
            {
                Debug.LogError("NGUICustomTools.GetChild : Container =null");
                return null;
            }

            Transform pGameObjectTF = null;

            if (Container.name == gameobjectName)
                pGameObjectTF = Container.transform;
            else
            {
                // Find all Elements.
                Transform[] allChildren = Container.transform.GetComponentsInChildren<Transform>();
                foreach (Transform child in allChildren)
                {
                    if (child.name == gameobjectName)
                    {
                        if (pGameObjectTF == null)
                            pGameObjectTF = child;
                        else
                            Debug.LogWarning("Container[" + Container.name + "][" + gameobjectName + "]");
                    }
                }
            }

            return pGameObjectTF.gameObject;
        }

        public static void Attach(GameObject mGameObject, GameObject getGameObject, Vector3 zero)
        {
        
        }
    }

}