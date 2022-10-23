using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Players
{
    public interface IPlayerInput
    {
        Vector2 MoveVec { get; }
        bool PushedFire { get; }
        bool PushingFire { get; }
    }
}

