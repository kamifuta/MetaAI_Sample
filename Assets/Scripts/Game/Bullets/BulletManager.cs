using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game.Bullets
{
    public class BulletManager : MonoBehaviour
    {
        [SerializeField] private BulletGenerator bulletGenerator;

        private List<GameObject> playerBulletList = new List<GameObject>();
        private List<GameObject> enemyBulletList = new List<GameObject>();

        public GameObject GeneratePlayerBullet(Vector3 position)
        {
            GameObject bullet = null;

            if (playerBulletList.Any(x => !x.activeSelf))
            {
                bullet = playerBulletList.First(x => !x.activeSelf);
            }
            else
            {
                bullet = bulletGenerator.GeneratePlayerBullet();
            }

            bullet.transform.position = position;
            return bullet;
        }

        public GameObject GenerateEnemyBullet(Vector3 position)
        {
            GameObject bullet = null;

            if (enemyBulletList.Any(x => !x.activeSelf))
            {
                bullet = enemyBulletList.First(x => !x.activeSelf);
            }
            else
            {
                bullet = bulletGenerator.GenerateEnemyBullet();
            }

            bullet.transform.position = position;
            return bullet;
        }
    }
}

