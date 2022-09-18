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
    public class PlayerController : ControllerBase, IStartable, IDisposable
    {
        private IPlayerInput playerInput;
        private IPlayerMover playerMover;
        private IPlayerShooter playerShooter;
        private IPlayerHealth playerHealth;
        private IPlayerAnimation playerAnimation;

        [Inject]
        public PlayerController(IPlayerInput playerInput, IPlayerMover playerMover, IPlayerShooter playerShooter,
            IPlayerHealth playerHealth, IPlayerAnimation playerAnimation)
        {
            this.playerInput = playerInput;
            this.playerMover = playerMover;
            this.playerShooter = playerShooter;
            this.playerHealth = playerHealth;
            this.playerAnimation = playerAnimation;
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

            this.ObserveEveryValueChanged(x => x.playerInput.MoveVec)
                .ThrottleFirstFrame(1)
                .Subscribe(_ =>
                {
                    playerMover.Move(playerInput.MoveVec);
                })
                .AddTo(this);

            this.ObserveEveryValueChanged(x => x.playerHealth.IsDead)
                .Skip(1)
                .Where(x => x)
                .Subscribe(_ =>
                {
                    playerAnimation.PlayDieAnimation();
                    Dispose();
                })
                .AddTo(this);
        }

        public void Dispose()
        {

        }
    }
}

