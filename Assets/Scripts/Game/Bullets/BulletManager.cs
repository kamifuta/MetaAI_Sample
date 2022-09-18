using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game.Bullets
{
    public class BulletManager : MonoBehaviour
    {
        [SerializeField] private BulletGenerator bulletGenerator;

        private const float Bottom = -5.5f;
        private const float Up = 5.5f;

        private List<GameObject> playerBulletList = new List<GameObject>();
        private List<GameObject> enemyBulletList = new List<GameObject>();

        private void Start()
        {
            StartCoroutine(ObserveOutofFieldBulletCoroutine());
        }

        public GameObject GeneratePlayerBullet(Vector3 position)
        {
            GameObject bullet = null;

            if (playerBulletList.Any(x => !x.activeSelf))
            {
                bullet = playerBulletList.First(x => !x.activeSelf);
                bullet.SetActive(true);
            }
            else
            {
                bullet = bulletGenerator.GeneratePlayerBullet();
            }

            bullet.transform.position = position;
            playerBulletList.Add(bullet);
            return bullet;
        }

        public GameObject GenerateEnemyBullet(Vector3 position)
        {
            GameObject bullet = null;

            if (enemyBulletList.Any(x => !x.activeSelf))
            {
                bullet = enemyBulletList.First(x => !x.activeSelf);
                bullet.SetActive(true);
            }
            else
            {
                bullet = bulletGenerator.GenerateEnemyBullet();
            }

            bullet.transform.position = position;
            enemyBulletList.Add(bullet);
            return bullet;
        }

        private IEnumerator ObserveOutofFieldBulletCoroutine()
        {
            var interval = new WaitForSeconds(0.5f);

            while (true)
            {
                yield return interval;

                foreach(var bullet in playerBulletList)
                {
                    var posY = bullet.transform.position.y;
                    if (posY > Up || posY < Bottom)
                    {
                        bullet.SetActive(false);
                    }
                }

                foreach (var bullet in enemyBulletList)
                {
                    var posY = bullet.transform.position.y;
                    if (posY > Up || posY < Bottom)
                    {
                        bullet.SetActive(false);
                    }
                }
            }
        }
    }
}

