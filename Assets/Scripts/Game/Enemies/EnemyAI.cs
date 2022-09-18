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

        private Subject<bool> pushedFireSubject = new Subject<bool>();
        public IObservable<bool> PushedFire => pushedFireSubject.AsObservable();

        public EnemyAI(CancellationToken token)
        {
            PushedFireAsync(token).Forget();
        }

        public async UniTask PushedFireAsync(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: token);
            while (true)
            {
                pushedFireSubject.OnNext(true);
                await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken: token);
            }
        }

        public void Dispose()
        {
            pushedFireSubject.Dispose();
        }
    }
}
