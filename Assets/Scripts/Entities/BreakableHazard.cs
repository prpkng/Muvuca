using Muvuca.Core;
using Muvuca.Systems;
using UnityEngine;

namespace Muvuca.Entities
{
    public class BreakableHazard : Hazard
    {
        private void AttackPressed()
        {
            Destroy(gameObject);
            InputManager.AttackPressed -= AttackPressed;
        }

        protected override void Entered()
        {
            InputManager.AttackPressed += AttackPressed;
        }

        protected override void Exited()
        {
            base.Exited();
            InputManager.AttackPressed -= AttackPressed;
        }
    }
}
