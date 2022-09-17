using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private EnemyManager enemyManager;

        private void Start()
        {
            StartCoroutine(enemyManager.GenerateEnemyCoroutine());
        }
    }
}

