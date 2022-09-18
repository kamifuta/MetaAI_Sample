using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemies
{
    public class EnemyDamagable : MonoBehaviour, IEnemyDamagable
    {
        [SerializeField] private EnemyHealth enemyHealth;

        public void ApplyDamage(int damage)
        {
            enemyHealth.AddDamage(damage);
        }
    }
}

