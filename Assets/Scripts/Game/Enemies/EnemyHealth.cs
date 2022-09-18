using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemies
{
    public class EnemyHealth : MonoBehaviour, IEnemyHealth
    {
        [SerializeField] private int MaxHP;
        private int currentHP;

        public bool IsDead => currentHP <= 0;

        private void Start()
        {
            currentHP = MaxHP;
        }

        public void AddDamage(int damage)
        {
            currentHP -= damage;
        }
    }
}

