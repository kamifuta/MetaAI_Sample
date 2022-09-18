using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;
using System.Linq;

namespace Game.Managers
{
    public class MetaAI : MonoBehaviour
    {
        [SerializeField] private EnemyManager enemyManager;
        [SerializeField] private AnimationCurve curve;

        private List<float> dethPosList = new List<float>();

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
                await UniTask.WhenAll(
                    UniTask.Delay(TimeSpan.FromSeconds(10), cancellationToken: token),
                    UniTask.WaitUntil(() => dethPosList.Count >= 10)
                    );

                var result = CaluDifficulty();
                enemyManager.SetGenerateInterval(result);
            }
        }

        private float CaluDifficulty()
        {
            var average = dethPosList.Average();
            var value = curve.Evaluate((average / 10) + 0.5f);

            var result = (value - 0.5f) * 4;
            return result;
        }
    }
}

