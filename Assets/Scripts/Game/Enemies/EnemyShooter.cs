using Game.Bullets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemies
{
    public class EnemyShooter : MonoBehaviour, IEnemyShooter
    {
        [SerializeField] private GameObject bulletPrefab;

        public void Shot()
        {
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<BulletMover>().SetInitioalSpeed(Vector2.down);
        }
    }
}

