using Game.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class PlayerHealthView : MonoBehaviour
    {
        [SerializeField] private Image[] playerHealthImages;

        public void ViewPlayerHP(int hp)
        {
            int imageCount = playerHealthImages.Length;

            for(int i = 0; i < imageCount; i++)
            {
                if (i < hp)
                {
                    playerHealthImages[i].color = Color.green;
                }
                else
                {
                    playerHealthImages[i].color = Color.red;
                }
            }
        }
    }
}

