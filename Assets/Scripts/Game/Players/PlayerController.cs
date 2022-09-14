using MyUtil;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Players
{
    public class PlayerController : ControllerBase, IStartable, ITickable
    {
        private IPlayerInput playerInput;
        private IPlayerMover playerMover;
        private IPlayerShooter playerShooter;

        [Inject]
        public PlayerController(IPlayerInput playerInput, IPlayerMover playerMover, IPlayerShooter playerShooter)
        {
            this.playerInput = playerInput;
            this.playerMover = playerMover;
            this.playerShooter = playerShooter;
        }

        public void Start()
        {
            this.ObserveEveryValueChanged(x => x.playerInput.PushedFire)
                .ThrottleFirst(TimeSpan.FromSeconds(1))
                .Where(x => x)
                .Subscribe(_ =>
                {
                    playerShooter.Shot();
                })
                .AddTo(this);
        }

        public void Tick()
        {
            playerMover.Move(playerInput.MoveVec);
        }
    }
}

