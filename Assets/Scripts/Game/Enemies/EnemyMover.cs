using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemies
{
    public class EnemyMover : MonoBehaviour, IEnemyMover
    {
        [SerializeField] private Rigidbody2D rigidbody;
        //[SerializeField] private float moveSpeed;

        public void Move(Vector2 moveVec, float moveSpeed)
        {
            rigidbody.velocity = moveVec * moveSpeed;
        }
    }
}

