using Game.Managers.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Game.Managers.Items
{
    public class ItemManager : MonoBehaviour
    {
        [SerializeField] private EnemyManager enemyManager;
        [SerializeField] private ItemDropper itemDropper;

        private float itemDropRate = 0.1f;

        private void Start()
        {
            enemyManager.EnemyGeneratedObservable
                .Subscribe(enemy =>
                {
                    enemy.OnDestroyAsObservable()
                        .Subscribe(_ =>
                        {
                            DropItem(enemy.transform.position);
                        })
                        .AddTo(this);
                })
                .AddTo(this);
        }

        private void DropItem(Vector3 dropPoint)
        {
            var r = Random.value;
            if (r < itemDropRate)
            {
                itemDropper.DropItem(dropPoint);
            }
        }

        public void SetItemDropRate(float rate)
        {
            itemDropRate = rate;
        }
    }
}

