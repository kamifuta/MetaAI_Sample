using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Managers
{
    public class EnemyGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;

        public GameObject GenerateEnemy(Vector2 generatePos)
        {
            var enemy = Instantiate(enemyPrefab, generatePos, Quaternion.identity);
            return enemy;
        }
    }
}

