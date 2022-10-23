using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Players
{
    public class PlayerKyeInput : IPlayerInput
    {
        public Vector2 MoveVec => new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        public bool PushedFire => Input.GetKeyDown(KeyCode.Space);
        public bool PushingFire => Input.GetKey(KeyCode.Space);
    }
}

