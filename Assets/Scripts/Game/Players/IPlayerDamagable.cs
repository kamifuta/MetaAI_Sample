using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Players
{
    public interface IPlayerDamagable
    {
        void ApplyDamage(int damage);
    }
}

