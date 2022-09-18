using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Players
{
    public class PlayerAnimation : MonoBehaviour, IPlayerAnimation
    {
        public void PlayDieAnimation()
        {
            Destroy(gameObject);
        }
    }
}

