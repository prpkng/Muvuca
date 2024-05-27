using FMODUnity;
using Muvuca.Core;
using UnityEngine;

namespace Muvuca.Game.Elements.Enemies
{
    public class BreakableHazard : Hazard
    {
        [SerializeField]
        private StudioEventEmitter emitter;
        private void AttackPressed()
        {
            Destroy(gameObject);
            emitter.Play();
            InputManager.AttackPressed -= AttackPressed;
        }

        protected override void Entered()
        {
            InputManager.AttackPressed += AttackPressed;
        }

        protected override void Exited()
        {
            Damage();
            InputManager.AttackPressed -= AttackPressed;
        }
    }
}
