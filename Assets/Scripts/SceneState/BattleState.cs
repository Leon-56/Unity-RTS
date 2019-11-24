using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : ISceneState
{
    public BattleState(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "BattleState";
    }

    // Start.
    public override void StateBegin()
    {
        RTSGame.Instance.Initinal();
    }

    // End.
    public override void StateEnd()
    {
        RTSGame.Instance.Release();
    }

    // Update.
    public override void StateUpdate()
    {
        // Input.
        InputProcess();

        // Game Logic.
        RTSGame.Instance.Update();

        // Render by Unity.

        // Is Game Over ?
        if (RTSGame.Instance.ThisGameIsOver())
            m_Controller.SetState(new MainMenuState(m_Controller), "MainMenuScene");
    }

    private void InputProcess()
    {
        // Player Inputs.
    }

}