using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigirdbody;
    [SerializeField] private float moveSpeed;

    public void Move(Vector2 moveVec)
    {
        rigirdbody.velocity = moveVec * moveSpeed;
    }
}
