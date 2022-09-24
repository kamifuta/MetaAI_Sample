using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Players
{
    public class PlayerDamagable : MonoBehaviour, IPlayerDamagable
    {
        [SerializeField] private PlayerHealth playerHealth;

        public void ApplyDamage(int damage)
        {
            playerHealth.AddDamage(damage);
        }
    }
}

