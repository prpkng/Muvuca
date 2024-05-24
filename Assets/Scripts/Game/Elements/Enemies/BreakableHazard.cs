using Muvuca.Core;

namespace Muvuca.Game.Elements.Enemies
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
            Damage();
            InputManager.AttackPressed -= AttackPressed;
        }
    }
}
