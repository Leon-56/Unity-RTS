using RTS.GameSystem.Soldier;
using UnityEngine;

namespace RTS.GameSystem.Character
{
    public class SoldierOnClick : MonoBehaviour
    {
        public ISoldier Solder = null;

        // Use this for initialization
        void Start () {	
        }
	
        // Update is called once per frame
        void Update () {	
        }

        public void OnClick()
        {
            //Debug.Log ("CharacterOnClick.OnClick:" + gameObject.name);
            RTSGame.Instance.ShowSoldierInfo( Solder );
        }
    }
}