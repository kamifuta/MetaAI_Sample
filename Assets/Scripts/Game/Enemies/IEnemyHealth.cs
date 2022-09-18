using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemies
{
    public interface IEnemyHealth
    {
        bool IsDead { get; }
        void AddDamage(int damage);
    }
}

