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

        private WaitForSeconds enemyGenerateInterval = new WaitForSeconds(1f);

        public IEnumerator GenerateEnemyCoroutine()
        {
            while (true)
            {
                Vector2 generatePos = new Vector2(Random.Range(-4f, 4f), 5.5f);
                enemyGenerator.GenerateEnemy(generatePos);
                yield return enemyGenerateInterval;
            }
        }
    }
}

