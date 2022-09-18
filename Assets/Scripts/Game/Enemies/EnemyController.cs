using MyUtil;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Enemies
{
    public class EnemyController : ControllerBase, IDisposable
    {
        private IEnemyMover enemyMover;
        private IEnemyInput enemyAI;
        private IEnemyShooter enemyShooter;
        private IEnemyHealth enemyHealth;
        private IEnemyAnimation enemyAnimation;

        public EnemyController(IEnemyMover enemyMover, IEnemyInput enemyAI, IEnemyShooter enemyShooter,
            IEnemyHealth enemyHealth, IEnemyAnimation enemyAnimation)
        {
            this.enemyMover = enemyMover;
            this.enemyAI = enemyAI;
            this.enemyShooter = enemyShooter;
            this.enemyHealth = enemyHealth;
            this.enemyAnimation = enemyAnimation;

            Init();
        }

        private void Init()
        {
            enemyMover.Move(enemyAI.MoveVec);

            enemyAI.PushedFire
                .Where(x => x)
                .Subscribe(_ =>
                {
                    enemyShooter.Shot();
                })
                .AddTo(this);

            this.ObserveEveryValueChanged(x => x.enemyHealth.IsDead)
                .Skip(1)
                .Where(x => x)
                .Subscribe(_ =>
                {
                    enemyAnimation.PlayDieAnimation();   
                })
                .AddTo(this);
        }

        public void Dispose()
        {
            
        }
    }
}

