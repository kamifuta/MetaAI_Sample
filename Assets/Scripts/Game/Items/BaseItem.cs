using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using Game.Managers.Enemies;

namespace Game.Items
{
    public class BaseItem : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D rigidbody;

        protected EnemyManager enemyManager;

        public void Init(EnemyManager enemyManager, float speed)
        {
            this.enemyManager = enemyManager;
            rigidbody.velocity = new Vector3(0, speed, 0);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(TagType.Player))
            {
                Execute();
            }
        }

        protected virtual void Execute()
        {
            Debug.Log("Item Execute!");
        }
    }
}

