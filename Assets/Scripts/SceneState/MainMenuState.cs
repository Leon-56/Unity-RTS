using System.Collections;
using System.Collections.Generic;
using RTS.Scene;
using UnityEngine;
using UnityEngine.UI;

namespace RTS.Scene
{
    public class MainMenuState : ISceneState
    {
        public MainMenuState(SceneStateController Controller) : base(Controller)
        {
            this.StateName = "MainMenuState";
        }

        // Start.
        public override void StateBegin()
        {
            // Get start button.
            Button tempBtn = UITool.GetUIComponent<Button>("StartGameBtn");
            if (tempBtn != null)
                tempBtn.onClick.AddListener(
                    () => OnStartGameBtnClick(tempBtn)
                );

        }

        // Start Game.
        private void OnStartGameBtnClick(Button theButton)
        {
            //Debug.Log("OnStartGameBtnClick:" + theButton.gameObject.name);
            m_Controller.SetState(new BattleState(m_Controller), "BattleScene");
        }

    } 
}