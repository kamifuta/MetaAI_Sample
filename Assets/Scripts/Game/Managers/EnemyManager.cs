using Cysharp.Threading.Tasks;
using Game.Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Threading;

namespace Game.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyGenerator enemyGenerator; 

        private WaitForSeconds enemyGenerateInterval = new WaitForSeconds(8f);

        private CancellationTokenSource _AILinkedToken;

        public IEnumerator GenerateEnemyCoroutine()
        {
            yield return null;
            while (true)
            {
                Vector2 generatePos = new Vector2(Random.Range(-4f, 4f), 5.5f);
                GameObject enemy=enemyGenerator.GenerateEnemy(generatePos);

                InitEnemy(enemy);
                
                yield return enemyGenerateInterval;
            }
        }

        private void InitEnemy(GameObject enemy)
        {
            var linkedToken = CancellationTokenSource.CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy(), new CancellationToken());

            EnemyMover enemyMover = enemy.GetComponent<EnemyMover>();
            EnemyShooter enemyShooter = enemy.GetComponent<EnemyShooter>();
            EnemyAI enemyAI = new EnemyAI(linkedToken.Token);
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            EnemyAnimation enemyAnimation = enemy.GetComponent<EnemyAnimation>();

            EnemyController enemyController = new EnemyController(enemyMover, enemyAI, enemyShooter, enemyHealth, enemyAnimation);
            ObservaEnemyObject(enemy, enemyController, linkedToken);
        }

        private void ObservaEnemyObject(GameObject enemy, EnemyController enemyController, CancellationTokenSource token)
        {
            enemy.OnDestroyAsObservable()
                .Subscribe(_ =>
                {
                    enemyController.Dispose();
                    token.Cancel();
                })
                .AddTo(this);
        }
    }
}

