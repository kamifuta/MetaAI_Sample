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
using Game.Managers.Players;

namespace Game.Managers.Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyGenerator enemyGenerator;
        [SerializeField] private PlayerManager playerManager;

        private WaitForSeconds enemyGenerateInterval;
        [SerializeField] private float intervalValue =2.5f;

        private float enemyMoveSpeed = 1f;
        private float enemyShortShotInterval = 0.8f;
        private float enemyLongShotInterval = 2.4f;

        public const float MaxInterval = 4f;
        public const float MinInterval = 1f;

        private const float Bottom = -5.5f;

        private ISubject<Vector2> enemyDethPointSubject = new Subject<Vector2>();
        public IObservable<Vector2> EnemyDethPointObservable => enemyDethPointSubject;

        private List<GameObject> currentEnemyList = new List<GameObject>();
        public IReadOnlyList<GameObject> CurrentEnemyList => currentEnemyList;

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
                currentEnemyList.Add(enemy);
                
                yield return enemyGenerateInterval;
            }
        }

        private void InitEnemy(GameObject enemy)
        {
            EnemyAI enemyAI = new EnemyAI(enemyMoveSpeed, enemyShortShotInterval, enemyLongShotInterval);

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
                    playerManager.AddDamage();
                })
                .AddTo(this);

            enemy.OnDestroyAsObservable()
                .Subscribe(_ =>
                {
                    Vector3 position = enemy.transform.position;
                    enemyDethPointSubject.OnNext(new Vector2(position.x, position.y));
                    enemyController.Dispose();
                    enemyInput.Dispose();
                    currentEnemyList.Remove(enemy);
                })
                .AddTo(this);
        }

        public void SetGenerateInterval(float interval)
        {
            intervalValue =interval;
            //intervalValue = Mathf.Clamp(intervalValue, MinInterval, MaxInterval);
            enemyGenerateInterval = new WaitForSeconds(intervalValue);
            Debug.Log($"interval: {intervalValue}");
        }

        public void AdjustEnemyAI(float moveSpeed, float shortShotInterval, float longShotInterval)
        {
            enemyMoveSpeed = moveSpeed;
            enemyShortShotInterval = shortShotInterval;
            enemyLongShotInterval = longShotInterval;
        }
    }
}

