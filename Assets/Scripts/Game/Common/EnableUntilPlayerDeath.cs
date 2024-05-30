using System;
using Muvuca.Core;
using Muvuca.Systems;
using UnityEngine;
using UnityEngine.Events;

namespace Muvuca.Game.Common
{
    public class EnableUntilPlayerDeath : MonoBehaviour
    {
        [SerializeField] private HitboxChecker hitbox;
        [SerializeField] private UnityEvent enable;
        [SerializeField] private UnityEvent disable;

        private bool didDisable = false;
        
        private void OnEnable()
        {
            hitbox.entered += Entered;
        }

        private void OnDisable()
        {
            hitbox.entered -= Entered;
        }

        private void Entered()
        {
            if (didDisable) return;
            LevelManager.onLevelReset += () =>
            {
                enable.Invoke();
                didDisable = false;
            };
            didDisable = true;
            disable.Invoke();
        }
    }
}