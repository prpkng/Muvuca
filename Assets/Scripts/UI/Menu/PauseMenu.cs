using System;
using DG.Tweening;
using Muvuca.Core;
using UnityEngine;

namespace Muvuca.UI.Menu
{
    public class PauseMenu : MonoBehaviour
    {
        private bool paused = false;
        public void Pause()
        {
            paused = true;
            DOTween.To(() => Time.timeScale, f => Time.timeScale = f, 0f, 0.5f).SetEase(Ease.OutExpo).SetUpdate(true).SetTarget(transform);
        }

        public void Unpause()
        {
            transform.DOKill(); 
            DOTween.To(() => Time.timeScale, f => Time.timeScale = f, 1f, 0f)
                .SetEase(Ease.OutExpo)
                .SetUpdate(true)
                .SetTarget(transform)
                .onComplete += () =>
            {
                paused = false;
                InputManager.IgnoringMouse = false;
            };
        }

        private void Update()
        {
            if (paused) 
                InputManager.IgnoringMouse = true;
        }
    }
}