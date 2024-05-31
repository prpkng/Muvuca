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
            hitbox.exited += Exited;
        }

        private void OnDisable()
        {
            hitbox.entered -= Entered;
            hitbox.exited -= Exited;
        }

        private void Entered()
        {
            InputManager.AttackPressed += AttackPressed;
        }

        private void Exited()
        {
            InputManager.AttackPressed -= AttackPressed;
        }


        private void AttackPressed()
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