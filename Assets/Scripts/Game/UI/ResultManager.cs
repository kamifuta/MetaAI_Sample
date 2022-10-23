using Game.Managers.Players;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.UI
{
    public class ResultManager : MonoBehaviour
    {
        [SerializeField] private Button retryButton;
        [SerializeField] private GameObject resultPanel;

        private void Start()
        {
            retryButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    resultPanel.SetActive(false);
                    SceneManager.LoadScene("GameScene");
                })
                .AddTo(this);
        }

        public void ViewPanel()
        {
            resultPanel.SetActive(true);
        }
    }
}

