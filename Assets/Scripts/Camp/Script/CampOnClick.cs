using UnityEngine;

namespace RTS.GameSystem.Camp
{
    public class CampOnClick : MonoBehaviour
    {
        public ICamp theCamp = null;

        // Use this for initialization
        void Start () {}
	
        // Update is called once per frame
        void Update () {}

        public void OnClick()
        {
            // 顯示兵營資訊
            RTSGame.Instance.ShowCampInfo( theCamp );
        }
    }
}