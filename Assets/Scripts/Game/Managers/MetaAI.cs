using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using Game.Managers.Enemies;
using Game.Players;
using Game.Managers.Items;

namespace Game.Managers
{
    public class MetaAI : MonoBehaviour
    {
        [SerializeField] private EnemyManager enemyManager;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private ItemManager itemManager;
        [SerializeField] private AnimationCurve tensityCurve;

        //private List<float> dethPosList = new List<float>();

        private const float AdjustInterval = 1f;

        [SerializeField] private DataSender dataSender;
        private bool firstSend = false;
        private int sendDataCount = 1;

        private void Start()
        {

            //enemyManager.EnemyDethPointObservable
            //    .Subscribe(pos =>
            //    {
            //        dethPosList.Add(pos.y);
            //    })
            //    .AddTo(this);

            AdjustDifficultyAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTask AdjustDifficultyAsync(CancellationToken token)
        {
            while (true)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(AdjustInterval), cancellationToken:token);
                //await UniTask.WaitUntil(() => dethPosList.Count >= 5);
                if (playerHealth.IsDead) break;

                var tensity = CalcTensity();
                Debug.Log($"tensity:{tensity}");
                AdjustDifficulty(tensity);
                //dethPosList.Clear();
            }
        }

        private float CalcTensity()
        {
            var deathPositionTensity = CalcTensityBottomEnemyPosition();
            var playerHealthTensity = CalcTensityPlayerHealth();
            var enemyCountTensity = CalcTensityEnemyCount();
            float tensity = deathPositionTensity + playerHealthTensity + enemyCountTensity;

            var data = new TensityData(tensity, deathPositionTensity, playerHealthTensity, enemyCountTensity);
            dataSender.SendData(TensityData.userName, sendDataCount, data);
            sendDataCount++;

            return tensity;
        }

        /// <summary>
        /// �ł���ʉ��ɂ���G�̈ʒu�ɂ��ْ��x
        /// </summary>
        /// <returns>0����0.4�܂ł̒l��Ԃ�</returns>
        private float CalcTensityBottomEnemyPosition()
        {
            if (enemyManager.CurrentEnemyList.Count == 0)
            {
                return 0.01f;
            }

            var bottomPosY = enemyManager.GetButtomEnemy(1).First().transform.position.y;
            var result = tensityCurve.Evaluate((bottomPosY / 10) + 0.5f);

            Debug.Log($"tensity deth Pos:{result}");
            return result;
        }

        /// <summary>
        /// �v���C���[�̗̑͂ɂ��ْ��x
        /// </summary>
        /// <returns>0����0.2�܂ł̒l��Ԃ�</returns>
        private float CalcTensityPlayerHealth()
        {
            var result = 0.04f * (5-playerHealth.currentHP);
            Debug.Log($"tensity player HP:{result}");
            return result;
        }

        /// <summary>
        /// �G�̐��ɂ��ْ��x
        /// </summary>
        /// <returns>0����0.4�܂ł̒l��Ԃ�</returns>
        private float CalcTensityEnemyCount()
        {
            var enemyCount = enemyManager.CurrentEnemyList.Count;
            if (enemyCount == 0)
            {
                return 0.01f;
            }

            if (enemyCount > 5)
            {
                return 0.4f;
            }

            var result = 0.08f * enemyCount;
            Debug.Log($"tensity enemy count:{result}");
            return result;
        }

        private void AdjustDifficulty(float tensity)
        {
            //�G�̐����Ԋu�𒲐�
            enemyManager.SetGenerateInterval(CalcGenerateInterval(tensity));

            //�G�̒e�̔��ˊԊu�A�����X�s�[�h�𒲐�
            var shotInterval = CalcShotInterval(tensity);
            enemyManager.AdjustEnemyAI(CalcMoveSpeed(tensity), shotInterval.Key, shotInterval.Value);

            //�A�C�e���̃h���b�v���𒲐�
            itemManager.SetItemDropRate(CalcitemDropRate(tensity));
        }

        private float CalcGenerateInterval(float tensity)
        {
            //(0,0.5),(1,2.0)��ʂ�A(0.5,1.25)��ϋȓ_�Ƃ���֐�
            var result = (2f / 5f) * Mathf.Pow((tensity - 0.5f), 3) + 1.25f;
            return result;
        }

        private float CalcMoveSpeed(float tensity)
        {
            var result = 0.4f * tensity + 0.8f;
            return result;
        }

        private KeyValuePair<float, float> CalcShotInterval(float tensity)
        {
            var shortShotInterval = 0.4f * tensity + 0.6f;
            var longShotInterval = 0.4f * (1 / tensity) + 2.2f;

            return new KeyValuePair<float, float>(shortShotInterval, longShotInterval);
        }

        private float CalcitemDropRate(float tensity)
        {
            var result = Mathf.Pow(tensity - 0.55f, 3) + 0.15f;
            return result;
        }
    }
}

