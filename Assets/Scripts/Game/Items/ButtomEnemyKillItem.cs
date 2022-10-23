using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items
{
    public class ButtomEnemyKillItem : BaseItem
    {
        protected override void Execute()
        {
            base.Execute();

            var targetEnemies = enemyManager.GetButtomEnemy(3);
            foreach(var enemy in targetEnemies)
            {
                Destroy(enemy);
            }

            Destroy(gameObject);
        }
    }
}

