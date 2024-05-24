using System.Collections;
using Muvuca.Core;
using Muvuca.Systems;
using UnityEngine;

namespace Muvuca.Game.Player
{
    public class PlayerInputBuffering : MonoBehaviour
    {
        [SerializeField] private float bufferingTime;
        public static Vector3? BufferedPosition;
        private void OnEnable()
        {
            InputManager.JumpPressed += JumpPressed;
        }

        private void OnDisable()
        {
            InputManager.JumpPressed -= JumpPressed;
        }

        private void JumpPressed()
        {
            if (PlayerController.Instance.hasPlatform) return;
            BufferedPosition = PlatformSelector.Instance.targetPosition;
            StopAllCoroutines();
            StartCoroutine(Deactivate());
        }

        private IEnumerator Deactivate()
        {
            yield return new WaitForSeconds(bufferingTime);
            BufferedPosition = null;
        }
    }
}