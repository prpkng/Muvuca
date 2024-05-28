using System;
using Muvuca.Systems;
using UnityEngine;

namespace Muvuca.Game.Elements
{
    public abstract class HitboxListener : MonoBehaviour
    {
        public HitboxChecker hitbox;

        protected virtual void OnEnable()
        {
            hitbox.entered += Entered;
            hitbox.exited += Exited;
        }

        protected virtual void OnDisable()
        {
            hitbox.entered -= Entered;
            hitbox.exited -= Exited;
        }

        protected virtual void Exited() { }

        protected virtual void Entered() { }
    }
}