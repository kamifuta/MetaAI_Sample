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
        private IEnemyInput enemyAI;
        private IEnemyMover enemyMover;
        private IEnemyShooter enemyShooter;
        private IEnemyHealth enemyHealth;
        private IEnemyAnimation enemyAnimation;

        public EnemyController(IEnemyInput enemyAI, GameObject enemyObj)
        {
            
            this.enemyAI = enemyAI;
            this.enemyMover = enemyObj.GetComponent<IEnemyMover>();
            this.enemyShooter = enemyObj.GetComponent<IEnemyShooter>();
            this.enemyHealth = enemyObj.GetComponent<IEnemyHealth>();
            this.enemyAnimation = enemyObj.GetComponent<IEnemyAnimation>();

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

