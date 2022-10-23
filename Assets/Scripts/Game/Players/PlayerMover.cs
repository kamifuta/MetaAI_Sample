using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;
using UniRx;

namespace Game.Players
{
    public class PlayerMover : MonoBehaviour, IPlayerMover
    {
        [SerializeField] private Rigidbody2D rigirdbody;
        [SerializeField] private float moveSpeed;

        private const float limitHorizontal = 4.5f;

        public void Start()
        {
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    var x = Mathf.Clamp(transform.position.x, -limitHorizontal, limitHorizontal);
                    transform.position = new Vector3(x, transform.position.y, 0);
                })
                .AddTo(this);
        }

        public void Move(Vector2 moveVec)
        {
            rigirdbody.velocity = moveVec * moveSpeed;
        }
    }
}

