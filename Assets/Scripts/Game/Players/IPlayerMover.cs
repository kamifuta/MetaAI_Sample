using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Players
{
    public interface IPlayerMover
    {
        void Move(Vector2 moveVec);
    }
}

