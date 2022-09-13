using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKyeInput : MonoBehaviour
{
    public Vector2 MoveVec => new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
    public bool PushedFire => Input.GetKeyDown(KeyCode.Space);
}
