using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Players
{
    public interface IPlayerHealth
    {
        int currentHP { get; }
        bool IsDead { get; }

        void AddDamage(int damage);
    }
}

