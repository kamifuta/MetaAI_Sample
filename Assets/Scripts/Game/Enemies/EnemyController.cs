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
        [Inject] private IEnemyInput enemyInput;

        private IEnemyMover enemyMover;
        private IEnemyShooter enemyShooter;
        private IEnemyHealth enemyHealth;
        private IEnemyAnimation enemyAnimation;

        public EnemyController(IEnemyInput enemyInput, GameObject enemyObj)
        {
            
            this.enemyInput = enemyInput;
            this.enemyMover = enemyObj.GetComponent<IEnemyMover>();
            this.enemyShooter = enemyObj.GetComponent<IEnemyShooter>();
            this.enemyHealth = enemyObj.GetComponent<IEnemyHealth>();
            this.enemyAnimation = enemyObj.GetComponent<IEnemyAnimation>();

            Init();
        }

        public void Init()
        {
            enemyMover.Move(enemyInput.MoveVec, enemyInput.MoveSpeed);

            enemyInput.PushedFire
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

