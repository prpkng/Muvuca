using DG.Tweening;
using Muvuca.Game.Elements.Platform;
using UnityEngine;

namespace Muvuca.Game.Player
{
    public class ReturnState : PlayerState
    {
        private bool canReturn;
        public async override void Enter(string[] data = null)
        {
            canReturn = false;
            player.enteredPlatform += EnteredPlatform;
            if (player.lastSafePlatform != player.platform)
            {
                await player.transform.DOScale(0f, .5f).SetEase(Ease.OutCubic).AsyncWaitForCompletion();
                player.transform.position = player.lastSafePlatform.position;
                await player.transform.DOScale(1f, .5f).SetEase(Ease.OutCubic).AsyncWaitForCompletion();

                EnteredPlatform(player.lastSafePlatform);
                return;
            }

            canReturn = true;
        }
        public override void Exit()
        {
            player.enteredPlatform -= EnteredPlatform;
            canReturn = false;
        }

        private void EnteredPlatform(Transform platform)
        {
            player.platform = platform;
            player.transform.position = platform.position;
            if (platform.TryGetComponent(out LaunchPlatform plat))
                plat.hasPlayer = true;
            if (platform.GetChild(0).TryGetComponent(out plat))
            {
                plat.hasPlayer = true;
                player.platform = platform.GetChild(0);
            }

            machine.ChangeState("idle", null);
        }
        public override void Update()
        {
            if (!canReturn) return;
            player.transform.position += (player.lastSafePlatform.position - player.transform.position) *
                                        (1f - Mathf.Exp(-player.returnSpeed * Time.deltaTime));
        }
    }
}