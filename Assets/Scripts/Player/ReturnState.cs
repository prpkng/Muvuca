using Muvuca.Entities.Platform;
using UnityEngine;

namespace Muvuca.Player
{
    public class ReturnState : PlayerState
    {
        public override void Enter(string[] data = null)
        {
            player.enteredPlatform += EnteredPlatform;
        }
        public override void Exit()
        {
            player.enteredPlatform -= EnteredPlatform;
        }

        private void EnteredPlatform(Transform platform)
        {
            player.platform = platform;
            player.transform.position = platform.position;
            if (platform.TryGetComponent(out LaunchPlatform plat))
                plat.hasPlayer = true;

            machine.ChangeState("idle", null);
        }
        public override void Update()
        {
            player.transform.position += (player.platform.position - player.transform.position) *
                                        (1f - Mathf.Exp(-player.returnSpeed * Time.deltaTime));
        }
    }
}