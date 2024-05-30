using System;
using Cysharp.Threading.Tasks;
using Muvuca.Core;
using Muvuca.Game.Player;
using TMPro;
using UnityEngine;

namespace Muvuca.UI.HUD
{
    public class PlayerLiveCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text tmp;

        private void OnEnable()
        {
            PlayerLives.LifeRemoved += PlayerLivesOnLifeRemoved;
        }
        private void OnDisable()
        {
            PlayerLives.LifeRemoved -= PlayerLivesOnLifeRemoved;
        }

        private async void PlayerLivesOnLifeRemoved()
        {
            await UniTask.WaitForEndOfFrame();
            tmp.text = $"x{PlayerLives.LifeCount}";
        }
    }
}