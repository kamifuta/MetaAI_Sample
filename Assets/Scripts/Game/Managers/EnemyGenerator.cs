using Cysharp.Threading.Tasks;
using Game.Enemies;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using VContainer;

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

