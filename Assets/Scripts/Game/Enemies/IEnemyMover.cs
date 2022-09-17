using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemies
{
    public interface IEnemyMover
    {
        public void Move(Vector2 moveVec);
    }
}

