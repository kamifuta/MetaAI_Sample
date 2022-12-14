using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemies
{
    public interface IEnemyInput
    {
        Vector2 MoveVec { get; }
        float MoveSpeed { get; }
        IObservable<bool> PushedFire { get; }
    }
}

