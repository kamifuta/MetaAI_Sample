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

namespace Game.Managers.Enemies
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

        private void Start()
        {
            StartCoroutine(GenerateEnemyCoroutine());
        }

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
            //var linkedToken = CancellationTokenSource.CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy(), new CancellationToken());

            EnemyAI enemyAI = new EnemyAI();

            EnemyController enemyController = new EnemyController(enemyAI, enemy);
            ObservaEnemyObject(enemy, enemyController, enemyAI);
        }

        private void ObservaEnemyObject(GameObject enemy, EnemyController enemyController, IDisposable enemyInput)
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
                    enemyInput.Dispose();
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

