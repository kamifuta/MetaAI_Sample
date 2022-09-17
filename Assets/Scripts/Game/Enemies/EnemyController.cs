using MyUtil;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Enemies
{
    public class EnemyController : ControllerBase, IStartable
    {
        private IEnemyMover enemyMover;
        private IEnemyInput enemyAI;
        private IEnemyShooter enemyShooter;

        public EnemyController(IEnemyMover enemyMover, IEnemyInput enemyAI, IEnemyShooter enemyShooter)
        {
            this.enemyMover = enemyMover;
            this.enemyAI = enemyAI;
            this.enemyShooter = enemyShooter;

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
        }

        public void Start()
        {
            
        }
    }
}

