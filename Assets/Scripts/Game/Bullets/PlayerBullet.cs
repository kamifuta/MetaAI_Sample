using Game.Enemies;
using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Bullets
{
    public class PlayerBullet : BaseBullet
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<IEnemyDamagable>(out var damagable))
            {
                damagable.ApplyDamage(1);
                //Destroy(this.gameObject);
                gameObject.SetActive(false);
            }
        }
    }
}

