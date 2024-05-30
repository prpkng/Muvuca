using Muvuca.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Muvuca.Game.Elements
{
    public class DisableOnUse : HitboxListener
    {
        [SerializeField] private UnityEvent onDisable;
        [SerializeField] private UnityEvent onEnable;
        protected override void Exited()
        {
            InputManager.AttackPressed -= AttackPressed;
        }

        protected override void Entered()
        {
            InputManager.AttackPressed += AttackPressed;
        }

        private void AttackPressed()
        {
            onDisable.Invoke();
            LevelManager.onLevelReset += () =>
                onEnable.Invoke();
        }
    }
}