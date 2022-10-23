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

        private List<GameObject> itemObjList = new List<GameObject>();

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
                var item=itemDropper.DropItem(dropPoint);
                itemObjList.Add(item);
            }
        }

        public void SetItemDropRate(float rate)
        {
            itemDropRate = rate;
        }

        private void OnDestroy()
        {
            foreach(var item in itemObjList)
            {
                Destroy(item);
            }
        }
    }
}

