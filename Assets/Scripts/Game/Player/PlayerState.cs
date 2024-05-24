using Muvuca.Core;
using Muvuca.Systems;

namespace Muvuca.Game.Player
{
    public class PlayerState : State
    {
        public PlayerController player => (PlayerController)machine.owner;
    }
}