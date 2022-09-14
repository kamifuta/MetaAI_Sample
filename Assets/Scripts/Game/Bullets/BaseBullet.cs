using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Bullets
{
    public abstract class BaseBullet : MonoBehaviour
    {
        abstract protected void OnTriggerEnter2D(Collider2D other);
    }
}

