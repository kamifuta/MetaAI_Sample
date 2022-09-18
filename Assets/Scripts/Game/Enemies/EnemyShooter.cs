using Game.Bullets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemies
{
    public class EnemyShooter : MonoBehaviour, IEnemyShooter
    {
        private BulletManager bulletManager;

        private void Awake()
        {
            bulletManager = FindObjectOfType<BulletManager>();
        }

        public void Shot()
        {
            var bullet = bulletManager.GenerateEnemyBullet(transform.position);
            bullet.GetComponent<BulletMover>().SetInitioalSpeed(Vector2.down);
        }
    }
}

