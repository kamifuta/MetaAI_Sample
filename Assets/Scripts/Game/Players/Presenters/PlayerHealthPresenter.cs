using Game.UI;
using MyUtil;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Players.Presenters
{
    public class PlayerHealthPresenter : ControllerBase, IStartable
    {
        private readonly IPlayerHealth playerHealth;
        private readonly PlayerHealthView playerHealthView;

        [Inject]
        public PlayerHealthPresenter(GameObject playerObj, PlayerHealthView playerHealthView)
        {
            playerHealth = playerObj.GetComponent<IPlayerHealth>();
            this.playerHealthView = playerHealthView;
        }

        public void Start()
        {
            this.ObserveEveryValueChanged(x => x.playerHealth.currentHP)
                .Subscribe(x =>
                {
                    playerHealthView.ViewPlayerHP(x);
                })
                .AddTo(this);
        }
    }
}

