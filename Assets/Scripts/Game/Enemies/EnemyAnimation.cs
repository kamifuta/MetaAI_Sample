using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemies
{
    public class EnemyAnimation : MonoBehaviour, IEnemyAnimation
    {
        public void PlayDieAnimation()
        {
            Destroy(gameObject);
        }
    }
}

