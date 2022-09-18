using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemies
{
    public interface IEnemyDamagable
    {
        void ApplyDamage(int damage);
    }
}

