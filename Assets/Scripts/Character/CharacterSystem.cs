using UnityEngine;
using System;
using System.Collections;

public class CharacterSystem : IGameSystem
{
    public CharacterSystem(RTSGame RTS) : base(RTS) { }

    public int GetEnemyCount()
    {

        return 0;
    }
}
