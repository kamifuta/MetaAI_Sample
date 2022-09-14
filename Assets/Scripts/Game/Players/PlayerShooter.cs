using Game.Bullets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour, IPlayerShooter
{
    [SerializeField] private GameObject BulletPrefab;

    public void Shot()
    {
        var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<BulletMover>().SetInitioalSpeed(Vector2.up);
    }
}
