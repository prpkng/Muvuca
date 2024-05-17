using Muvuca.Core;
using Muvuca.Systems;

namespace Muvuca.Player
{
    public class PlayerState : State
    {
        public PlayerController player => (PlayerController)machine.owner;
    }
}