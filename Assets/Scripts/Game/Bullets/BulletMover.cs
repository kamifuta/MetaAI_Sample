using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Bullets
{
    public class BulletMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody;
        [SerializeField] private float moveSpeed;

        public void SetInitioalSpeed(Vector2 moveVec)
        {
            rigidbody.velocity = moveVec * moveSpeed;
        }
    }
}

