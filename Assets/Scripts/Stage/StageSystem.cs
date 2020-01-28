using System.Collections;
using System.Collections.Generic;
using RTS;
using RTS.GameSystem;
using UnityEngine;

public class StageSystem : IGameSystem
{
    public StageSystem(RTSGame RTS) : base(RTS) { }

    public void SetEnemyKilledCount(int KilledCount)
    {
        return ;
    }
}
