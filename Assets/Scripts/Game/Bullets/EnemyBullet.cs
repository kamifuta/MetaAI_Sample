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
            if (collision.CompareTag(TagType.Player))
            {
                Destroy(this.gameObject);
            }
        }
    }
}

