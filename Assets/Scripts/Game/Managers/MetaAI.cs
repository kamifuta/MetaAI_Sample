using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using Game.Managers.Enemies;
using Game.Players;

namespace Game.Managers
{
    public class MetaAI : MonoBehaviour
    {
        [SerializeField] private EnemyManager enemyManager;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private AnimationCurve tensityCurve;

        private List<float> dethPosList = new List<float>();

        private const float AdjustInterval = 5f;

        private void Start()
        {
            enemyManager.EnemyDethPointObservable
                .Subscribe(pos =>
                {
                    dethPosList.Add(pos.y);
                })
                .AddTo(this);

            AdjustDifficultyAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTask AdjustDifficultyAsync(CancellationToken token)
        {
            while (true)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(AdjustInterval), cancellationToken:token);
                //await UniTask.WaitUntil(() => dethPosList.Count >= 5);

                var tensity = CalcTensity();
                Debug.Log($"tensity:{tensity}");
                AdjustDifficulty(tensity);
                dethPosList.Clear();
            }
        }

        private float CalcTensity()
        {
            float tensity = CalcTensityDethPosition() + CalcTensityPlayerHealth() + CalcTensityEnemyCount();
            return tensity;
        }

        /// <summary>
        /// 敵を倒した位置による緊張度
        /// </summary>
        /// <returns>0から0.4までの値を返す</returns>
        private float CalcTensityDethPosition()
        {
            if (dethPosList.Count == 0)
            {
                return 0.4f;
            }

            var average = dethPosList.Average();
            var result = tensityCurve.Evaluate((average / 10) + 0.5f);

            Debug.Log($"tensity deth Pos:{result}");
            return result;
        }

        /// <summary>
        /// プレイヤーの体力による緊張度
        /// </summary>
        /// <returns>0から0.2までの値を返す</returns>
        private float CalcTensityPlayerHealth()
        {
            var result = 0.04f * (5-playerHealth.currentHP);
            Debug.Log($"tensity player HP:{result}");
            return result;
        }

        /// <summary>
        /// 敵の数による緊張度
        /// </summary>
        /// <returns>0から0.4までの値を返す</returns>
        private float CalcTensityEnemyCount()
        {
            var enemyCount = enemyManager.CurrentEnemyList.Count;
            if (enemyCount == 0)
            {
                return 0.01f;
            }

            if (enemyCount > 5)
            {
                return 0.4f;
            }

            var result = 0.08f * enemyCount;
            Debug.Log($"tensity enemy count:{result}");
            return result;
        }

        private void AdjustDifficulty(float tensity)
        {
            enemyManager.SetGenerateInterval(CalcGenerateInterval(tensity));
            var shotInterval = CalcShotInterval(tensity);
            enemyManager.AdjustEnemyAI(CalcMoveSpeed(tensity), shotInterval.Key, shotInterval.Value);
        }

        private float CalcGenerateInterval(float tensity)
        {
            var result = (EnemyManager.MaxInterval - EnemyManager.MinInterval) * Mathf.Pow((tensity-0.05f),2) + EnemyManager.MinInterval;
            return result;
        }

        private float CalcMoveSpeed(float tensity)
        {
            var result = 0.4f * tensity + 0.8f;
            return result;
        }

        private KeyValuePair<float, float> CalcShotInterval(float tensity)
        {
            var shortShotInterval = 0.4f * tensity + 0.6f;
            var longShotInterval = 0.4f * (1 / tensity) + 2.2f;

            return new KeyValuePair<float, float>(shortShotInterval, longShotInterval);
        }
    }
}

