using Game.Bullets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Players
{
    public class PlayerShooter : MonoBehaviour, IPlayerShooter
    {
        [SerializeField] private BulletManager bulletManager;

        public void Shot()
        {
            var bullet = bulletManager.GeneratePlayerBullet(transform.position);
            bullet.GetComponent<BulletMover>().SetInitioalSpeed(Vector2.up);
        }
    }
}

