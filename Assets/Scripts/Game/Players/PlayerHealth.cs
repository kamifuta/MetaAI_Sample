using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Players
{
    public class PlayerHealth : MonoBehaviour, IPlayerHealth
    {
        [SerializeField] private int maxHP=5;
        private int currentHP;

        public bool IsDead => currentHP <= 0;

        private void Start()
        {
            currentHP = maxHP;
        }

        public void AddDamage(int damage)
        {
            currentHP -= damage;
        }
    }
}
