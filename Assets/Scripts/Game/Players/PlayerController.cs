using MyUtil;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
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

        private bool IsShooting = false;

        [Inject]
        public PlayerController(IPlayerInput playerInput, GameObject playerObj)
        {
            this.playerInput = playerInput;
            this.playerMover = playerObj.GetComponent<IPlayerMover>();
            this.playerShooter = playerObj.GetComponent<IPlayerShooter>();
            this.playerHealth = playerObj.GetComponent<IPlayerHealth>();
            this.playerAnimation = playerObj.GetComponent<IPlayerAnimation>();
        }

        public void Start()
        {
            this.ObserveEveryValueChanged(x => x.playerInput.PushedFire)
                .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
                .Where(x => x && !IsShooting)
                .Subscribe(_ =>
                {
                    playerShooter.Shot();
                    IsShooting = true;

                    //スペースキーを押し続けている間弾を発射し続ける
                    Observable.Interval(TimeSpan.FromSeconds(0.4f))
                    .TakeWhile(_ => playerInput.PushingFire)
                        .Subscribe(_ =>
                        {
                            playerShooter.Shot();
                        }, 
                        () => 
                        {
                            IsShooting = false;
                            Debug.Log("finish");
                         })
                        .AddTo(this);
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

