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

        private WaitForSeconds enemyGenerateInterval;
        [SerializeField] private float intervalValue =2.5f;

        private const float MaxInterval = 4f;
        private const float MinInterval = 1f;

        private const float Bottom = -5.5f;

        private ISubject<Vector2> enemyDethPointSubject = new Subject<Vector2>();
        public IObservable<Vector2> EnemyDethPointObservable => enemyDethPointSubject;

        public IEnumerator GenerateEnemyCoroutine()
        {
            enemyGenerateInterval = new WaitForSeconds(intervalValue);
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
            enemy.UpdateAsObservable()
                .Where(_=>enemy.transform.position.y<Bottom)
                .Subscribe(_ =>
                {
                    Destroy(enemy);
                })
                .AddTo(this);

            enemy.OnDestroyAsObservable()
                .Subscribe(_ =>
                {
                    Vector3 position = enemy.transform.position;
                    enemyDethPointSubject.OnNext(new Vector2(position.x, position.y));
                    enemyController.Dispose();
                    token.Cancel();
                })
                .AddTo(this);
        }

        public void SetGenerateInterval(float delta)
        {
            intervalValue -= delta;
            intervalValue = Mathf.Clamp(intervalValue, MinInterval, MaxInterval);
            enemyGenerateInterval = new WaitForSeconds(intervalValue);
            Debug.Log($"interval: {intervalValue}");
        }
    }
}

