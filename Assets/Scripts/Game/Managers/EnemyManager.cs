using Cysharp.Threading.Tasks;
using Game.Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyGenerator enemyGenerator; 

        private WaitForSeconds enemyGenerateInterval = new WaitForSeconds(8f);

        public IEnumerator GenerateEnemyCoroutine()
        {
            yield return null;
            while (true)
            {
                Vector2 generatePos = new Vector2(Random.Range(-4f, 4f), 5.5f);
                GameObject enemy=enemyGenerator.GenerateEnemy(generatePos);

                EnemyMover enemyMover = enemy.GetComponent<EnemyMover>();
                EnemyShooter enemyShooter = enemy.GetComponent<EnemyShooter>();
                EnemyAI enemyAI = new EnemyAI(this.GetCancellationTokenOnDestroy());

                new EnemyController(enemyMover, enemyAI, enemyShooter);

                yield return enemyGenerateInterval;
            }
        }
    }
}

