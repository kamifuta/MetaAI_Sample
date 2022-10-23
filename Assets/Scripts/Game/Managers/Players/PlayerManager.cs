using Game.Players;
using Game.UI;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Game.Managers.Players
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private GameObject playerObj;
        [SerializeField] private ResultManager resultManager;

        private IPlayerHealth playerHealth;

        private void Start()
        {
            playerHealth = playerObj.GetComponent<IPlayerHealth>();

            this.ObserveEveryValueChanged(x => x.playerHealth.IsDead)
                .Where(x => x)
                .Subscribe(_ =>
                {
                    resultManager.ViewPanel();
                })
                .AddTo(this);
        }

        public void AddDamage()
        {
            playerHealth?.AddDamage(1);
        }
    }
}

