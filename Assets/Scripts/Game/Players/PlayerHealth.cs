using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Players
{
    public class PlayerHealth : MonoBehaviour, IPlayerHealth
    {
        [SerializeField] private int maxHP=5;
        public int currentHP { get; private set; }

        public bool IsDead => currentHP <= 0;

        private void Awake()
        {
            currentHP = maxHP;
        }

        public void AddDamage(int damage)
        {
            currentHP -= damage;
        }
    }
}
