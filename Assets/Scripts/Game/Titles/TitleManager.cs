using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Titles
{
    public class TitleManager : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private TMP_InputField inputField;

        private void Start()
        {
            startButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    LoadGameScene();
                })
                .AddTo(this);

            inputField.onValueChanged
                .AddListener(s =>
                {
                    TensityData.userName = s;
                });
        }

        public void LoadGameScene()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}

