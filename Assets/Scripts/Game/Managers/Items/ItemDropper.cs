using Game.Items;
using Game.Managers.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Managers.Items
{
    public class ItemDropper : MonoBehaviour
    {
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private EnemyManager enemyManager;

        public void DropItem(Vector3 dropPoint)
        {
            var item = Instantiate(itemPrefab, dropPoint, Quaternion.identity);
            item.GetComponent<BaseItem>().Init(enemyManager, -1f);
        }
    }
}

