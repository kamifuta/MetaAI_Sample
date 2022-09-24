using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace Game.Enemies
{
    public class EnemyAI : IEnemyInput, IDisposable
    {
        public Vector2 MoveVec { get; } = Vector2.down;

        public float MoveSpeed { get; private set; }
        private float shortShotInterval;
        private float longShotInterval;

        private Subject<bool> pushedFireSubject = new Subject<bool>();
        public IObservable<bool> PushedFire => pushedFireSubject.AsObservable();

        private readonly CancellationTokenSource tokenSource= new CancellationTokenSource();

        public EnemyAI(float moveSpeed, float shortShotInterval, float longShotInterval)
        {
            this.MoveSpeed = moveSpeed;
            this.shortShotInterval = shortShotInterval;
            this.longShotInterval = longShotInterval;

            PushedFireAsync(tokenSource.Token).Forget();
        }

        private async UniTask PushedFireAsync(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: token);
            while (true)
            {
                pushedFireSubject.OnNext(true);
                await UniTask.Delay(TimeSpan.FromSeconds(shortShotInterval), cancellationToken: token);
                pushedFireSubject.OnNext(true);
                await UniTask.Delay(TimeSpan.FromSeconds(longShotInterval), cancellationToken: token);
            }
        }

        public void Dispose()
        {
            pushedFireSubject.Dispose();
            tokenSource.Cancel();
        }
    }
}
