using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Bullets
{
    public class BulletGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject playerBulletprefab;
        [SerializeField] private GameObject enemyBulletprefab;

        public GameObject GeneratePlayerBullet()
        {
            return Instantiate(playerBulletprefab);
        }

        public GameObject GenerateEnemyBullet()
        {
            return Instantiate(enemyBulletprefab);
        }
    }
}

