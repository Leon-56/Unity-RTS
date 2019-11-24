using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : ISceneState
{
    public StartState(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "StartState";
    }

    // Satrt.
    public override void StateBegin()
    {
        // Load gameData and Init.
    }

    // Update.
    public override void StateUpdate()
    {
        m_Controller.SetState(new MainMenuState(m_Controller), "MainMenuScene");
    }
}
