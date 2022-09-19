using Game.Players;
using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Bullets
{
    public class EnemyBullet : BaseBullet
    {
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IPlayerDamagable>(out var damagable))
            {
                damagable.ApplyDamage(1);
                gameObject.SetActive(false);
            }
        }
    }
}

