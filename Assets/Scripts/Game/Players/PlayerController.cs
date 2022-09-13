using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Players
{
    public class PlayerController : ITickable
    {
        private IPlayerInput playerInput;
        private IPlayerMover playerMover;

        [Inject]
        public PlayerController(IPlayerInput playerInput, IPlayerMover playerMover)
        {
            this.playerInput = playerInput;
            this.playerMover = playerMover;
        }

        public void Tick()
        {
            playerMover.Move(playerInput.MoveVec);
        }
    }
}

