using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Muvuca.Core;
using Muvuca.Systems;
using Muvuca.Tools;
using UnityEngine;

namespace Muvuca.Player
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