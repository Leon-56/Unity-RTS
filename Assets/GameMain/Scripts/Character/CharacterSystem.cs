using UnityEngine;
using System;
using System.Collections;
using RTS;

namespace RTS.GameSystem
{
    public class CharacterSystem : IGameSystem
    {
        public CharacterSystem(RTSGame RTS) : base(RTS) { }

        public int GetEnemyCount()
        {
            return 0;
        }
    }
}